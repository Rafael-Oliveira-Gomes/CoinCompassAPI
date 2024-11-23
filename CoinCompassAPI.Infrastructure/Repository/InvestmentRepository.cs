using System.Data.Entity;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class InvestmentRepository : BaseRepository<Investment>, IInvestimentoRepository
    {
        public InvestmentRepository(DataContext context) : base(context) { }
        public async Task<IEnumerable<Investment>> ConsultarInvestimento(int skip, int take)
        {
            return await _context.Investimentos.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Investment> ConsultarInvestimentoPorID(int id)
        {
            return await _context.Investimentos.FindAsync(id);
        }
    }
}
