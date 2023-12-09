namespace InsuranceApp.Data.Models
{
    public class Insurance
    {
        public long Id { get; set; }
        public string? Provider { get; set; }
        public string? PolicyNumber { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        public List<Motor> Motors { get; set; } = new List<Motor>();
    }

    public class InsuranceEdit
    {
        public Insurance Insurance { get; set; }
        public List<Motor> Motors { get; set; }
    }

}
