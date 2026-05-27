namespace TripWise.Api.Models.Dto
{
    public class CreateDestinationDto
    {
        required
        public string DestinationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class UpdateDestinationDto
    {
        required
        public string DestinationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
