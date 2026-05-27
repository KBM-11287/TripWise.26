using TripWise.Api.DTOs.Destinations;
using TripWise.Api.Mappings;
using TripWise.Api.Repositories;

namespace TripWise.Api.Services
{
    public class DestinationService
    {
        private readonly IDestinationRepository _destinations;

        public DestinationService(IDestinationRepository destinations)
        {
            _destinations = destinations;
        }

        public async Task<List<DestinationResponse>> GetAllAsync()
        {
            var list = await _destinations.GetAllAsync();
            return list.Select(d => d.ToDestinationResponse()).ToList();
        }

        public async Task<DestinationResponse> CreateAsync(DestinationDto.Create model)
        {
            var dest = model.ToDestination();
            await _destinations.CreateAsync(dest);
            return dest.ToDestinationResponse();
        }

        public async Task<DestinationResponse> UpdateAsync(string id, DestinationDto.Update model)
        {
            var dest = await _destinations.GetByIdAsync(id);
            if (dest == null) return null;

            model.ApplyTo(dest);
            await _destinations.UpdateAsync(dest);

            return dest.ToDestinationResponse();
        }

        public Task DeleteAsync(string id) => _destinations.DeleteAsync(id);
    }
}
