using LegacyRenewalApp.Segments;

namespace LegacyRenewalApp
{
    public class SegmentFactory
    {
        public ISegment Create(string segmentName)
        {
            return segmentName switch
            {
                "Silver" => new SilverSegment(),
                "Gold" => new GoldSegment(),
                "Platinum" => new PlatinumSegment(),
                "Education" => new EducationSegment(),
                _ => new StandardSegment()
            };
        }
    }
}