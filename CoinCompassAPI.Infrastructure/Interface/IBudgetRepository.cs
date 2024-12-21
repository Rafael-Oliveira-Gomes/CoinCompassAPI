using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Interface
{
    public interface IBudgetRepository : IBaseRepository<Budget>
    {
        Task<IEnumerable<Budget>> ConsultarOrcamentos(int skip, int take);
        Task<Budget> ConsultarOrcamentoPorID(int id);
        Task<Budget> ConsultarOrcamentoPorUsuarioId(string usuarioId);

    }
}
