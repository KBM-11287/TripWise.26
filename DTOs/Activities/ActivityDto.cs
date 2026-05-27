namespace TripWise.Api.DTOs.Activities
{

    public class CreateActivityDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = null!;
    }

    public class UpdateActivityDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; } = null!;
    }
   
}
