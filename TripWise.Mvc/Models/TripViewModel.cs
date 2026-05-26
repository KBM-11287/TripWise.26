namespace TripWise.Mvc.Models
{
    public class TripViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ActivityViewModel> Activities { get; set; }
    }
}
