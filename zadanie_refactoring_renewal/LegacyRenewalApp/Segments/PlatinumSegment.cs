namespace LegacyRenewalApp.Segments;

public class PlatinumSegment : ISegment
{
    public string name =>  "Platinum";

    public decimal setDiscountAmount(decimal baseDisc)
    {
        baseDisc *= 0.15m;
        return baseDisc;
    }

    public string addNote(string note)
    {
        note+= "platinum discount; ";
        return note;
    }
}