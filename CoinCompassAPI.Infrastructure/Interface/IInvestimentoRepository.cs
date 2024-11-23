using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Interface
{
    public interface IInvestimentoRepository : IBaseRepository<Investment>
    {
        Task<IEnumerable<Investment>> ConsultarInvestimento(int skip, int take);
        Task<Investment> ConsultarInvestimentoPorID(int id);
    }
}
