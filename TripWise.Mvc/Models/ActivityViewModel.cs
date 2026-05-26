namespace TripWise.Mvc.Models
{
    public class ActivityViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
