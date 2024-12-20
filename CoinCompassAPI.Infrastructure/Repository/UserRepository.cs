using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context) { }
        public async Task<List<ApplicationUser>> ListUsers()
        {
            List<ApplicationUser> list = await _context.User.ToListAsync();

            return list;
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            ApplicationUser user = await _context.User.FindAsync(userId);

            return user;
        }
    }
}
