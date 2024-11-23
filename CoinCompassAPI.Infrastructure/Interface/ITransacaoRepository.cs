using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Interface
{
    public interface ITransacaoRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> ConsultarTransacao(int skip, int take);
        Task<Transaction> ConsultarTansacaoPorID(int id);
    }
}
