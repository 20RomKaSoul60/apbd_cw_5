namespace LegacyRenewalApp.Segments;

public class EducationSegment : ISegment
{
    public string name =>  "Education";

    public decimal setDiscountAmount(decimal baseDisc,SubscriptionPlan plan)
    {
        if(plan.IsEducationEligible){
            baseDisc *= 0.20m;
        }
        return baseDisc;
    }

    public string addNote(string note)
    {
        note += "education discount; ";
        return note;
    }

    
}