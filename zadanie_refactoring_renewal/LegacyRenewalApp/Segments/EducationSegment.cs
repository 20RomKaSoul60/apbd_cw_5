namespace LegacyRenewalApp.Segments
{
    public class EducationSegment : ISegment
    {
        public decimal CalculateDiscount(decimal baseAmount, SubscriptionPlan plan)
        {
            if (plan.IsEducationEligible)
            {
                return baseAmount * 0.20m;
            }

            return 0m;
        }

        public string GetNote(SubscriptionPlan plan)
        {
            if (plan.IsEducationEligible)
            {
                return "education discount; ";
            }

            return string.Empty;
        }
    }
}