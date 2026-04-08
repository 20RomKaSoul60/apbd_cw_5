namespace LegacyRenewalApp.Discounts
{
    public class LargeTeamDiscount : TeamDiscount
    {
        public decimal GetDiscount(decimal amount)
        {
            return amount * 0.12m;
        }

        public string GetNote()
        {
            return "large team discount; ";
        }
    }
}