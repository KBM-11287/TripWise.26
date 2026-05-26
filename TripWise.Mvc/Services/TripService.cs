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

        public Task<List<TripViewModel>> GetTripsAsync()
        {
            return _api.GetAsync<List<TripViewModel>>("/api/v1/trips");
        }

        public Task<TripViewModel> GetTripByIdAsync(string id)
        {
            return _api.GetAsync<TripViewModel>($"/api/v1/trips/{id}");
        }
    }
}
