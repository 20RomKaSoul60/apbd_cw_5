using System;

namespace LegacyRenewalApp
{
    public class PaymentFeeCalculator
    {
        public decimal Calculate(string normalizedPaymentMethod, decimal amountBase)
        {
            return normalizedPaymentMethod switch
            {
                "CARD" => amountBase * 0.02m,
                "BANK_TRANSFER" => amountBase * 0.01m,
                "PAYPAL" => amountBase * 0.035m,
                "INVOICE" => 0m,
                _ => throw new ArgumentException("Unsupported payment method")
            };
        }

        public string GetNote(string normalizedPaymentMethod)
        {
            return normalizedPaymentMethod switch
            {
                "CARD" => "card payment fee; ",
                "BANK_TRANSFER" => "bank transfer fee; ",
                "PAYPAL" => "paypal fee; ",
                "INVOICE" => "invoice payment; ",
                _ => throw new ArgumentException("Unsupported payment method")
            };
        }
    }
}