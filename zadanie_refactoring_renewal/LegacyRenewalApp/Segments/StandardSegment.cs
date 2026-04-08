namespace LegacyRenewalApp.Segments
{
    public class StandardSegment : ISegment
    {
        public decimal CalculateDiscount(decimal baseAmount, SubscriptionPlan plan)
        {
            return 0m;
        }

        public string GetNote(SubscriptionPlan plan)
        {
            return string.Empty;
        }
    }
}