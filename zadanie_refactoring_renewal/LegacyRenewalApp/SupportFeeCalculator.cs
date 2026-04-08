namespace LegacyRenewalApp
{
    public class SupportFeeCalculator
    {
        public decimal Calculate(string normalizedPlanCode, bool includePremiumSupport)
        {
            if (!includePremiumSupport)
            {
                return 0m;
            }

            return normalizedPlanCode switch
            {
                "START" => 250m,
                "PRO" => 400m,
                "ENTERPRISE" => 700m,
                _ => 0m
            };
        }

        public string GetNote(bool includePremiumSupport)
        {
            return includePremiumSupport ? "premium support included; " : string.Empty;
        }
    }
}