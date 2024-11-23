using System.Data.Entity;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class BudgetRepository : BaseRepository<Budget>, IBudgetRepository
    {
        public BudgetRepository(DataContext context) : base(context) { }
        public async Task<Budget> ConsultarOrcamentoPorID(int id)
        {
            return await _context.Orcamentos.FindAsync(id);
        }

        public async Task<IEnumerable<Budget>> ConsultarOrcamentos(int skip, int take)
        {
            return await _context.Orcamentos.Skip(skip).Take(take).ToListAsync();
        }
    }
}
