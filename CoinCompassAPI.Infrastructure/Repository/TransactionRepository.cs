using System.Data.Entity;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Persistence;

namespace CoinCompassAPI.Infrastructure.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransacaoRepository
    {
        public TransactionRepository(DataContext context) : base(context) { }
        public async Task<Transaction> ConsultarTansacaoPorID(int id)
        {
            return await _context.Transacoes.FindAsync(id);
        }

        public async Task<IEnumerable<Transaction>> ConsultarTransacao(int skip, int take)
        {
            return await _context.Transacoes.Skip(skip).Take(take).ToListAsync();
        }
    }
}
