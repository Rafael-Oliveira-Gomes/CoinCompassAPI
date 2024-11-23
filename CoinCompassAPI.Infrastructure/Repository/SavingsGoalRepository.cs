using System.Data.Entity;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class SavingsGoalRepository : BaseRepository<SavingsGoal>, ISavingsGoalRepository
    {
        public SavingsGoalRepository(DataContext context) : base(context) { }
        public async Task<SavingsGoal> ConsultarMetaEconomiaPorID(int id)
        {
            return await _context.MetaEconomias.FindAsync(id);
        }

        public async Task<IEnumerable<SavingsGoal>> ConsultarMetasEconomias(int skip, int take)
        {
            return await _context.MetaEconomias.Skip(skip).Take(take).ToListAsync();
        }
    }
}
