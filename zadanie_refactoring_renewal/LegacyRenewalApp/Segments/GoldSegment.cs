namespace LegacyRenewalApp.Segments;

public class GoldSegment : ISegment
{
    public string name => "Gold";
    
    public decimal setDiscountAmount(decimal baseDisc,SubscriptionPlan plan)
    {
        baseDisc *= 0.10m;
        return baseDisc;
    }

    public string addNote(string note)
    {
        note += "gold discount; ";
        return note;
    }
}