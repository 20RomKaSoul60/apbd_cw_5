namespace LegacyRenewalApp
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Segment { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int YearsWithCompany { get; set; }
        public int LoyaltyPoints { get; set; }
        public bool IsActive { get; set; }

        public Customer()
        {
        }

        public Customer(
            int id,
            string fullName,
            string email,
            string segment,
            string country,
            int yearsWithCompany,
            int loyaltyPoints,
            bool isActive)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Segment = segment;
            Country = country;
            YearsWithCompany = yearsWithCompany;
            LoyaltyPoints = loyaltyPoints;
            IsActive = isActive;
        }
    }
}