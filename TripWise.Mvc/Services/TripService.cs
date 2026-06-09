using TripWise.Mvc.Models;

namespace TripWise.Mvc.Services
{
    public class TripService
    {
        private readonly ApiClientService _api;

        public TripService(ApiClientService api)
        {
            _api = api;
        }
        // Get all trips for the logged-in user
        public Task<List<TripViewModel>> GetTripsAsync()
        {
            return _api.GetAsync<List<TripViewModel>>("/api/v1/trips");
        }
        // Get a specific trip by ID
        public Task<TripViewModel> GetTripByIdAsync(string id)
        {
            return _api.GetAsync<TripViewModel>($"/api/v1/trips/{id}");
        }
        // Create a new trip
        public async Task<bool> CreateTripAsync(CreateTripViewModel model)
        {
            try
            {
                await _api.PostAsync<TripViewModel>("/api/v1/trips", model);
                return true;
            }
            catch
            {
                return false;
            }
        }
        // Update an existing trip
        public async Task<bool> UpdateTripAsync(EditTripViewModel model)
        {
            try
            {
                await _api.PutAsync<TripViewModel>($"/api/v1/trips/{model.Id}", model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete a trip by ID
        public async Task<bool> DeleteTripAsync(string id)
        {
            try
            {
                await _api.DeleteAsync($"/api/v1/trips/{id}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}