namespace LegacyRenewalApp.Segments
{
    public class PlatinumSegment : ISegment
    {
        public decimal CalculateDiscount(decimal baseAmount, SubscriptionPlan plan)
        {
            return baseAmount * 0.15m;
        }

        public string GetNote(SubscriptionPlan plan)
        {
            return "platinum discount; ";
        }
    }
}