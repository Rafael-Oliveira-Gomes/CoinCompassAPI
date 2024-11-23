using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Interface
{
    public interface ISavingsGoalRepository : IBaseRepository<SavingsGoal>
    {
        Task<IEnumerable<SavingsGoal>> ConsultarMetasEconomias(int skip, int take);
        Task<SavingsGoal> ConsultarMetaEconomiaPorID(int id);
    }
}
