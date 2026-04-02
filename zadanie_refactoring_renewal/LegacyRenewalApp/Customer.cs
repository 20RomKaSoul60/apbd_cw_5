namespace LegacyRenewalApp
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ISegment Segment { get; set; }
        public string Country { get; set; } = string.Empty;
        public int YearsWithCompany { get; set; }
        public int LoyaltyPoints { get; set; }
        public bool IsActive { get; set; }


        public Customer(int id, string fullName, string email, ISegment segment, string country, int yearsWithCompany, int loyaltyPoints, bool isActive)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;
            this.Segment = segment;
            this.Country = country;
            this.YearsWithCompany = yearsWithCompany;
            this.LoyaltyPoints = loyaltyPoints;
            this.IsActive = isActive;
        }
        
    }
}
