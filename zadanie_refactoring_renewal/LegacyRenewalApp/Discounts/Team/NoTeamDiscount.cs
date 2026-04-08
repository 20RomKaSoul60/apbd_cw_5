namespace LegacyRenewalApp.Discounts
{
    public class NoTeamDiscount : TeamDiscount
    {
        public decimal GetDiscount(decimal amount)
        {
            return 0m;
        }

        public string GetNote()
        {
            return string.Empty;
        }
    }
}