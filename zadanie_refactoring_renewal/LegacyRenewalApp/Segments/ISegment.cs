namespace LegacyRenewalApp
{
    public interface ISegment
    {
        decimal CalculateDiscount(decimal baseAmount, SubscriptionPlan plan);
        string GetNote(SubscriptionPlan plan);
    }
}