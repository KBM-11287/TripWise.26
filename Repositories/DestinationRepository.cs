using MongoDB.Driver;
using TripWise.Api.Models;

namespace TripWise.Api.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly IMongoCollection<Destination> _destinations;

        public DestinationRepository(IMongoDatabase database)
        {
            _destinations = database.GetCollection<Destination>("Destinations");
        }

        public async Task<Destination> GetByIdAsync(string id)
        {
            return await _destinations.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Destination>> GetAllAsync()
        {
            return await _destinations.Find(_ => true).ToListAsync();
        }

        public async Task CreateAsync(Destination destination)
        {
            await _destinations.InsertOneAsync(destination);
        }

        public async Task UpdateAsync(string id, Destination destination)
        {
            await _destinations.ReplaceOneAsync(d => d.Id == id, destination);
        }

        public async Task DeleteAsync(string id)
        {
            await _destinations.DeleteOneAsync(d => d.Id == id);
        }
    }
}