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

        public async Task<DestinationResponse> CreateAsync(CreateDestinationDto dto)
        {
            var destination = dto.ToDestination();
            await _destinations.CreateAsync(destination);
            return destination .ToDestinationResponse();
        }

        public async Task<DestinationResponse> UpdateAsync(string id, UpdateDestinationDto dto)
        {
            var destination = await _destinations.GetByIdAsync(id);
            if (destination == null) return null;

            dto.ApplyTo(destination);
            await _destinations.UpdateAsync(id, destination);   
            return destination.ToDestinationResponse();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _destinations.GetByIdAsync(id);
            if(existing == null) return false;
            await _destinations.DeleteAsync(id);
            return true;
        }
    }
}
