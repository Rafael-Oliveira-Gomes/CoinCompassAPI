using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Interface
{
    public interface IOutgoingRepository : IBaseRepository<Outgoings>
    {
        Task<IEnumerable<Outgoings>> ConsultarGastos(int skip, int take);
        Task<Outgoings> ConsultarGastosPorID(int id);
    }
}
