namespace TripWise.Api.DTOs.Destinations
{
    public class DestinationResponse
    {
        public string Id { get; set; }
        public string DestinationName { get; set; }

        // For simplicity, we return lat/lon directly in the response for easier MVC consumption.
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
