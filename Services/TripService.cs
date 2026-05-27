using TripWise.Api.DTOs.Trips;
using TripWise.Api.Mappings;
using TripWise.Api.Repositories;

namespace TripWise.Api.Services
{
    public class TripService
    {
        private readonly ITripRepository _trips;

        public TripService(ITripRepository trips)
        {
            _trips = trips;
        }

        public async Task<List<TripResponse>> GetAllAsync(string userId)
        {
            var trips = await _trips.GetAllByUserAsync(userId);
            return trips.Select(t => t.ToTripResponse()).ToList();
        }

        public async Task<TripResponse> GetByIdAsync(string id)
        {
            var trip = await _trips.GetByIdAsync(id);
            return trip?.ToTripResponse();
        }

        public async Task<TripResponse> CreateAsync(string userId, TripDto.Create model)
        {
            var trip = model.ToTrip(userId);
            await _trips.CreateAsync(trip);
            return trip.ToTripResponse();
        }

        public async Task<TripResponse> UpdateAsync(string id, TripDto.Update model)
        {
            var trip = await _trips.GetByIdAsync(id);
            if (trip == null) return null;

            model.ApplyTo(trip);
            await _trips.UpdateAsync(trip);

            return trip.ToTripResponse();
        }

        public Task DeleteAsync(string id) => _trips.DeleteAsync(id);
    }
}
