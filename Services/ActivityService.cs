using TripWise.Api.DTOs.Activities;
using TripWise.Api.Mappings;
using TripWise.Api.Repositories;

namespace TripWise.Api.Services
{
    public class ActivityService
    {
        private readonly ITripRepository _trips;

        public ActivityService(ITripRepository trips)
        {
            _trips = trips;
        }

        public async Task<ActivityResponse> AddAsync(string tripId, CreateActivityDto dto)
        {
            var trip = await _trips.GetByIdAsync(tripId);
            if (trip == null) return null;

            var activity = dto.ToActivity();
            trip.Activities.Add(activity);

            await _trips.UpdateAsync(tripId, trip);

            return activity.ToActivityResponse();
        }
        public async Task<ActivityResponse?> UpdateAsync(string tripId, string activityId, UpdateActivityDto dto)
        {
            var trip = await _trips.GetByIdAsync(tripId);
            if (trip == null)
                return null;

            var activity = trip.Activities.FirstOrDefault(a => a.Id == activityId);
            if (activity == null)
                return null;

            dto.ApplyTo(activity);

            await _trips.UpdateAsync(tripId, trip);

            return activity.ToActivityResponse();
        }

        public async Task<bool> DeleteAsync(string tripId, string activityId)
        {
            var trip = await _trips.GetByIdAsync(tripId);
            if (trip == null) return false;

           var removed =  trip.Activities.RemoveAll(a => a.Id == activityId) > 0;

            if(removed)
                await _trips.UpdateAsync(tripId, trip);

            return true;
        }
    }
}
