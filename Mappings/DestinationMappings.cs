using TripWise.Api.DTOs.Destinations;
using TripWise.Api.Models;

namespace TripWise.Api.Mappings
{
    public static class DestinationMappings
    {
        // CREATE DTO → Destination
        public static Destination ToDestination(this CreateDestinationDto dto)
        {
            return new Destination
            {
                DestinationName = dto.DestinationName,
                Coordinates = new GeoJsonPoint
                {
                    Coordinates = new[] { dto.Longitude, dto.Latitude }
                }
            };
        }

        // UPDATE DTO → apply to Destination
        public static void ApplyTo(this UpdateDestinationDto dto, Destination destination)
        {
            destination.DestinationName = dto.DestinationName;
            destination.Coordinates = new GeoJsonPoint
            {
                Coordinates = new[] { dto.Longitude, dto.Latitude }
            };
        }

        // Destination → DestinationResponse
        public static DestinationResponse ToDestinationResponse(this Destination destination)
        {
            return new DestinationResponse
            {
                Id = destination.Id,
                DestinationName = destination.DestinationName,
                Latitude = destination.Coordinates.Coordinates[1],
                Longitude = destination.Coordinates.Coordinates[0]
            };
        }
    }
}
