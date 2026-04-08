namespace LegacyRenewalApp.Segments
{
    public class SilverSegment : ISegment
    {
        public decimal CalculateDiscount(decimal baseAmount, SubscriptionPlan plan)
        {
            return baseAmount * 0.05m;
        }

        public string GetNote(SubscriptionPlan plan)
        {
            return "silver discount; ";
        }
    }
}