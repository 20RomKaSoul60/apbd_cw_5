namespace LegacyRenewalApp.Discounts
{
    public class DiscountManager
    {
        private readonly SegmentFactory _segmentFactory;

        public DiscountManager()
        {
            _segmentFactory = new SegmentFactory();
        }

        public decimal GetSegmentDiscount(Customer customer, SubscriptionPlan plan, decimal baseAmount)
        {
            var segment = _segmentFactory.Create(customer.Segment);
            return segment.CalculateDiscount(baseAmount, plan);
        }

        public string GetSegmentNote(Customer customer, SubscriptionPlan plan)
        {
            var segment = _segmentFactory.Create(customer.Segment);
            return segment.GetNote(plan);
        }

        public TeamDiscount GetTeamDiscount(int seatCount)
        {
            return seatCount switch
            {
                >= 50 => new LargeTeamDiscount(),
                >= 20 => new MediumTeamDiscount(),
                >= 10 => new SmallTeamDiscount(),
                _ => new NoTeamDiscount()
            };
        }

        public decimal GetLoyaltyYearsDiscount(Customer customer, decimal baseAmount)
        {
            if (customer.YearsWithCompany >= 5)
            {
                return baseAmount * 0.07m;
            }

            if (customer.YearsWithCompany >= 2)
            {
                return baseAmount * 0.03m;
            }

            return 0m;
        }

        public string GetLoyaltyYearsNote(Customer customer)
        {
            if (customer.YearsWithCompany >= 5)
            {
                return "long-term loyalty discount; ";
            }

            if (customer.YearsWithCompany >= 2)
            {
                return "basic loyalty discount; ";
            }

            return string.Empty;
        }

        public decimal GetLoyaltyPointsDiscount(Customer customer, bool useLoyaltyPoints)
        {
            if (!useLoyaltyPoints || customer.LoyaltyPoints <= 0)
            {
                return 0m;
            }

            int pointsToUse = customer.LoyaltyPoints > 200 ? 200 : customer.LoyaltyPoints;
            return pointsToUse;
        }

        public string GetLoyaltyPointsNote(Customer customer, bool useLoyaltyPoints)
        {
            if (!useLoyaltyPoints || customer.LoyaltyPoints <= 0)
            {
                return string.Empty;
            }

            int pointsToUse = customer.LoyaltyPoints > 200 ? 200 : customer.LoyaltyPoints;
            return $"loyalty points used: {pointsToUse}; ";
        }
    }
}