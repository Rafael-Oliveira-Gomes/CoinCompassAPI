using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Interface
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUser(string userId);
        Task<List<ApplicationUser>> ListUsers();
    }
}
