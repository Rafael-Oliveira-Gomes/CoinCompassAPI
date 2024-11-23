using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Interface
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<IEnumerable<Account>> ConsultarConta(int skip, int take);
        Task<Account> ConsultarContaoPorID(int id);
    }
}
