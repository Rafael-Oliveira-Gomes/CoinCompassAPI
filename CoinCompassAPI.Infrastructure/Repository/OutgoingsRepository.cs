using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class OutgoingsRepository : BaseRepository<Outgoings>, IOutgoingRepository
    {
        public OutgoingsRepository(DataContext context) : base(context)
        {
        }

        public Task<IEnumerable<Outgoings>> ConsultarGastos(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<Outgoings> ConsultarGastosPorID(int id)
        {
            return await _context.Gastos.FindAsync(id);
        }
    }
}
