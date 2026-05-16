using TripWise.Api.Models;

namespace TripWise.Api.Repositories
{
    public interface IDestinationRepository
    {
        Task<Destination> GetByIdAsync(string id);
        Task<List<Destination>> GetAllAsync();
        Task CreateAsync(Destination destination);
        Task UpdateAsync(string id, Destination destination);
        Task DeleteAsync(string id);
    }
}
