namespace InsuranceApp.Data.Models
{
    public class Motor
    {
        public long Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string RegistrationNumber { get; set; }

        public long InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
    }
}

