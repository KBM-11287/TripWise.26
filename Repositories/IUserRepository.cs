using TripWise.Api.Models;

namespace TripWise.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(string id);
        Task CreateAsync(User user);
    }
}
