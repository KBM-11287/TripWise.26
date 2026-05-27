using TripWise.Api.Models;

namespace TripWise.Api.Repositories
{
    public interface ITripRepository
    {
        Task<Trip> GetByIdAsync(string id);
        Task<List<Trip>> GetTripsForUserAsync(string userId);
        Task CreateAsync(Trip trip);
        Task UpdateAsync(string id, Trip trip);
        Task DeleteAsync(string id);

        // Activity operations
        Task<Activity> GetActivityByIdAsync(string tripId, string activityId);
        Task AddActivityAsync(string tripId, Activity activity);
        Task UpdateActivityAsync(string tripId, Activity activity);
        Task DeleteActivityAsync(string tripId, string activityId);
        Task<IEnumerable<object>> GetAllByUserAsync(string userId);
    }
}
