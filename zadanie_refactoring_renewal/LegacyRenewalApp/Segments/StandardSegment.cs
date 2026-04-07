namespace LegacyRenewalApp.Segments;

public class StandardSegment : ISegment
{
    public string name => "Standard";

    public decimal setDiscountAmount(decimal baseDisc,SubscriptionPlan plan)
    {
        return baseDisc;
    }

    public string addNote(string note)
    {
        return note;
    }
}