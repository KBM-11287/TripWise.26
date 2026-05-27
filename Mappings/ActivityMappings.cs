using TripWise.Api.DTOs.Activities;
using TripWise.Api.Models;

namespace TripWise.Api.Mappings
{
    public static class ActivityMappings
    {
        // CREATE DTO → Activity
        public static Activity ToActivity(this CreateActivityDto dto)
        {
            return new Activity
            {
                Name = dto.Name,
                Type = dto.Type,
                Description = dto.Description,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Location = dto.Location
            };
        }

        // UPDATE DTO → apply to Activity
        public static void ApplyTo(this UpdateActivityDto dto, Activity activity)
        {
            activity.Name = dto.Name;
            activity.Type = dto.Type;
            activity.Description = dto.Description;
            activity.StartTime = dto.StartTime;
            activity.EndTime = dto.EndTime;
            activity.Location = dto.Location;
        }

        // Activity → ActivityResponse
        public static ActivityResponse ToActivityResponse(this Activity activity)
        {
            return new ActivityResponse
            {
                Id = activity.Id,
                Name = activity.Name,
                Type = activity.Type,
                Description = activity.Description,
                Date = activity.Date,
                StartTime = activity.StartTime,
                EndTime = activity.EndTime,
                Location = activity.Location
            };
        }
    }
}
