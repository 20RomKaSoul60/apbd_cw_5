namespace LegacyRenewalApp;

public interface ISegment
{


    public decimal setDiscountAmount(decimal baseDisc,SubscriptionPlan plan);
    public string addNote(string note);



}