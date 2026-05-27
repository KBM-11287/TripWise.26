using TripWise.Api.DTOs.Activities;

namespace TripWise.Api.DTOs.Trips
{
    public class TripResponse
    {
        public string Id { get; set; }
        public string CreatorId { get; set; }
        public string Title { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // nested list of activities for the trip
        public List<ActivityResponse> Activities { get; set; } = new List<ActivityResponse>();
    }
}
