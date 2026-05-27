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
        public static void ApplyTo(this UpdateDestinationDto dto, Destination dest)
        {
            dest.DestinationName = dto.DestinationName;
            dest.Coordinates = new GeoJsonPoint
            {
                Coordinates = new[] { dto.Longitude, dto.Latitude }
            };
        }

        // Destination → DestinationResponse
        public static DestinationResponse ToDestinationResponse(this Destination dest)
        {
            return new DestinationResponse
            {
                Id = dest.Id,
                DestinationName = dest.DestinationName,
                Latitude = dest.Coordinates.Coordinates[1],
                Longitude = dest.Coordinates.Coordinates[0]
            };
        }
    }
}
