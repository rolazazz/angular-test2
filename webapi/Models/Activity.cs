namespace Test1.Models
{
    public class Activity
    {
        public Project Project { get; set; }

        public Employee Employee { get; set; }

        public DateTime? Date { get; set; }

        public int Hours { get; set; }
    }
}
