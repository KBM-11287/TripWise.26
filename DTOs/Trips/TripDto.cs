namespace TripWise.Api.DTOs.Trips
{
    public class CreateTripDto
    {
        public string Title { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class UpdateTripDto
    {
        public string Title { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
