namespace LegacyRenewalApp.Segments
{
    public class GoldSegment : ISegment
    {
        public decimal CalculateDiscount(decimal baseAmount, SubscriptionPlan plan)
        {
            return baseAmount * 0.10m;
        }

        public string GetNote(SubscriptionPlan plan)
        {
            return "gold discount; ";
        }
    }
}