namespace LegacyRenewalApp.Discounts
{
    public class MediumTeamDiscount : TeamDiscount
    {
        public decimal GetDiscount(decimal amount)
        {
            return amount * 0.08m;
        }

        public string GetNote()
        {
            return "medium team discount; ";
        }
    }
}