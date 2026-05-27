using TripWise.Api.Models;
using TripWise.Api.DTOs.Trips;
using TripWise.Api.DTOs.Activities;

namespace TripWise.Api.Mappings
{
    public static class TripMappings
    {
        // CREATE DTO -> Trip (domain)
        public static Trip ToTrip(this CreateTripDto dto, string createrId)
        {
            return new Trip
            {
                CreatorId = createrId,
                Title = dto.Title,
                Destination = dto.Destination,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Activities = new List<Activity>() // Initialize empty list for activities
            };
        }

        // UPDATE DTO -> apply changes to existing Trip 
        public static void ApplyTo(this UpdateTripDto dto, Trip trip)
        {
            trip.Title = dto.Title;
            trip.Destination = dto.Destination;
            trip.StartDate = dto.StartDate;
            trip.EndDate = dto.EndDate;
            // Note: Activities are not updated here, as they are managed separately
        }

        // Trip (domain) -> TripResponse (DTO)
        public static TripResponse ToTripResponse(this Trip trip)
        {
            return new TripResponse
            {
                Id = trip.Id,
                CreatorId = trip.CreatorId,
                Title = trip.Title,
                Destination = trip.Destination,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Activities = trip.Activities?
                .Select(a => a.ToActivityResponse())
                .ToList() ?? new List<ActivityResponse>() // Handle null activities list
            };
        }
    }
}
