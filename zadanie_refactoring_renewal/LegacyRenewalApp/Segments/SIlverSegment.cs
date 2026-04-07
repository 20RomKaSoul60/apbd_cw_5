namespace LegacyRenewalApp.Segments;

public class SilverSegment : ISegment
{
    public string name => "Silver";
    
    public decimal setDiscountAmount(decimal amount, SubscriptionPlan plan)
    {
        amount *= 0.05m;
        return amount;
    }

    public string addNote(string note)
    {
        note += "silver discount; ";
        return note;
    }
}