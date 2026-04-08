using System;
using LegacyRenewalApp.Discounts;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly SubscriptionPlanRepository _planRepository;
        private readonly RenewalRequestValidator _validator;
        private readonly DiscountManager _discountManager;
        private readonly SupportFeeCalculator _supportFeeCalculator;
        private readonly PaymentFeeCalculator _paymentFeeCalculator;
        private readonly TaxCalculator _taxCalculator;
        private readonly IBillingGateway _billingGateway;

        public SubscriptionRenewalService()
            : this(
                new CustomerRepository(),
                new SubscriptionPlanRepository(),
                new RenewalRequestValidator(),
                new DiscountManager(),
                new SupportFeeCalculator(),
                new PaymentFeeCalculator(),
                new TaxCalculator(),
                new LegacyBillingGatewayAdapter())
        {
        }

        public SubscriptionRenewalService(
            CustomerRepository customerRepository,
            SubscriptionPlanRepository planRepository,
            RenewalRequestValidator validator,
            DiscountManager discountManager,
            SupportFeeCalculator supportFeeCalculator,
            PaymentFeeCalculator paymentFeeCalculator,
            TaxCalculator taxCalculator,
            IBillingGateway billingGateway)
        {
            _customerRepository = customerRepository;
            _planRepository = planRepository;
            _validator = validator;
            _discountManager = discountManager;
            _supportFeeCalculator = supportFeeCalculator;
            _paymentFeeCalculator = paymentFeeCalculator;
            _taxCalculator = taxCalculator;
            _billingGateway = billingGateway;
        }

        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            _validator.Validate(customerId, planCode, seatCount, paymentMethod);

            string normalizedPlanCode = planCode.Trim().ToUpperInvariant();
            string normalizedPaymentMethod = paymentMethod.Trim().ToUpperInvariant();

            var customer = _customerRepository.GetById(customerId);
            var plan = _planRepository.GetByCode(normalizedPlanCode);

            if (!customer.IsActive)
            {
                throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
            }

            decimal baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
            decimal discountAmount = 0m;
            string notes = string.Empty;

            discountAmount += _discountManager.GetSegmentDiscount(customer, plan, baseAmount);
            notes += _discountManager.GetSegmentNote(customer, plan);

            var teamDiscount = _discountManager.GetTeamDiscount(seatCount);
            discountAmount += teamDiscount.GetDiscount(baseAmount);
            notes += teamDiscount.GetNote();

            discountAmount += _discountManager.GetLoyaltyYearsDiscount(customer, baseAmount);
            notes += _discountManager.GetLoyaltyYearsNote(customer);

            discountAmount += _discountManager.GetLoyaltyPointsDiscount(customer, useLoyaltyPoints);
            notes += _discountManager.GetLoyaltyPointsNote(customer, useLoyaltyPoints);

            decimal subtotalAfterDiscount = baseAmount - discountAmount;
            if (subtotalAfterDiscount < 300m)
            {
                subtotalAfterDiscount = 300m;
                notes += "minimum discounted subtotal applied; ";
            }

            decimal supportFee = _supportFeeCalculator.Calculate(normalizedPlanCode, includePremiumSupport);
            notes += _supportFeeCalculator.GetNote(includePremiumSupport);

            decimal paymentFeeBase = subtotalAfterDiscount + supportFee;
            decimal paymentFee = _paymentFeeCalculator.Calculate(normalizedPaymentMethod, paymentFeeBase);
            notes += _paymentFeeCalculator.GetNote(normalizedPaymentMethod);

            decimal taxRate = _taxCalculator.GetTaxRate(customer.Country);
            decimal taxBase = subtotalAfterDiscount + supportFee + paymentFee;
            decimal taxAmount = taxBase * taxRate;
            decimal finalAmount = taxBase + taxAmount;

            if (finalAmount < 500m)
            {
                finalAmount = 500m;
                notes += "minimum invoice amount applied; ";
            }

            var invoice = new RenewalInvoice
            {
                InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{customerId}-{normalizedPlanCode}",
                CustomerName = customer.FullName,
                PlanCode = normalizedPlanCode,
                PaymentMethod = normalizedPaymentMethod,
                SeatCount = seatCount,
                BaseAmount = Math.Round(baseAmount, 2, MidpointRounding.AwayFromZero),
                DiscountAmount = Math.Round(discountAmount, 2, MidpointRounding.AwayFromZero),
                SupportFee = Math.Round(supportFee, 2, MidpointRounding.AwayFromZero),
                PaymentFee = Math.Round(paymentFee, 2, MidpointRounding.AwayFromZero),
                TaxAmount = Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
                FinalAmount = Math.Round(finalAmount, 2, MidpointRounding.AwayFromZero),
                Notes = notes.Trim(),
                GeneratedAt = DateTime.UtcNow
            };

            _billingGateway.SaveInvoice(invoice);

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                string subject = "Subscription renewal invoice";
                string body =
                    $"Hello {customer.FullName}, your renewal for plan {normalizedPlanCode} " +
                    $"has been prepared. Final amount: {invoice.FinalAmount:F2}.";

                _billingGateway.SendEmail(customer.Email, subject, body);
            }

            return invoice;
        }
    }
}