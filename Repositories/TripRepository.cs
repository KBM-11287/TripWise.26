using MongoDB.Driver;
using TripWise.Api.Models;

namespace TripWise.Api.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly IMongoCollection<Trip> _trips;
        public TripRepository(IMongoDatabase database)
        {
            _trips = database.GetCollection<Trip>("Trips");
        }
        public async Task<List<Trip>> GetTripsForUserAsync(string userId)
        {
            return await _trips.Find(t => t.CreatorId == userId).SortBy(t => t.StartDate).ToListAsync();
        }

        public async Task<Trip> GetByIdAsync(string id)
        {
            return await _trips.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Trip trip)
        {
            await _trips.InsertOneAsync(trip);
        }

        public async Task UpdateAsync(Trip trip)
        {
            await _trips.ReplaceOneAsync(t => t.Id == trip.Id, trip);
        }

        public async Task DeleteAsync(string id)
        {
            await _trips.DeleteOneAsync(t => t.Id == id);
        }

        // Activity operations
        public async Task<Activity> GetActivityByIdAsync(string tripId, string activityId)
        {
            var trip = await GetByIdAsync(tripId);
            var activity = trip?.Activities.FirstOrDefault(a => a.Id == activityId);
            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with id '{activityId}' not found in trip '{tripId}'.");
            }
            return activity;
        }
        public async Task AddActivityAsync(string tripId, Activity activity)
        {
            var update = Builders<Trip>.Update.Push(t => t.Activities, activity);
            await _trips.UpdateOneAsync(t => t.Id == tripId, update);
        }
        public async Task UpdateActivityAsync(string tripId, Activity activity)
        {
            var filter = Builders<Trip>.Filter.And(
                Builders<Trip>.Filter.Eq(t => t.Id, tripId),
                Builders<Trip>.Filter.ElemMatch(t => t.Activities, a => a.Id == activity.Id)
            );
            var update = Builders<Trip>.Update.Set("activities.$", activity);
            await _trips.UpdateOneAsync(filter, update);
        }

        public async Task DeleteActivityAsync(string tripId, string activityId)
        {
            var update = Builders<Trip>.Update.PullFilter(t => t.Activities, a => a.Id == activityId);
            await _trips.UpdateOneAsync(t => t.Id == tripId, update);
        }
    }
}
