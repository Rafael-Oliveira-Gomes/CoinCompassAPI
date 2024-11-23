using System.Data.Entity;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Account>> ConsultarConta(int skip, int take)
        {
            return await _context.Contas.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Account> ConsultarContaoPorID(int id)
        {
            return await _context.Contas.FindAsync(id);
        }
    }
}
