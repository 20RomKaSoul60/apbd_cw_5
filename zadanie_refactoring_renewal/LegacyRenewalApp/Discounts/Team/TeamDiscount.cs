namespace LegacyRenewalApp.Discounts
{
    public interface TeamDiscount
    {
        decimal GetDiscount(decimal amount);
        string GetNote();
    }
}