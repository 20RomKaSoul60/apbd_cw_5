namespace LegacyRenewalApp.Discounts
{
    public class SmallTeamDiscount : TeamDiscount
    {
        public decimal GetDiscount(decimal amount)
        {
            return amount * 0.04m;
        }

        public string GetNote()
        {
            return "small team discount; ";
        }
    }
}