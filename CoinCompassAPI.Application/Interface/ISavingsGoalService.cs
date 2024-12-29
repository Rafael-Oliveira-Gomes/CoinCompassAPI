using CoinCompassAPI.Application.DTOs.SavingsGoal;

namespace CoinCompassAPI.Application.Interface
{
    public interface ISavingsGoalService
    {
        Task CadastrarSavingsGoal(CreateSavingsGoalDto SavingsGoalDto);
        Task<IEnumerable<CreateSavingsGoalDto>> ConsultarSavingsGoal(int skip = 0, int take = 20);
        Task<ReadSavingsGoalDto> ConsultarSavingsGoalPorID(int id);
        Task<bool> AtualizarSavingsGoal(int id, CreateSavingsGoalDto SavingsGoalDto);
        Task<bool> DeletarSavingsGoal(int id);
    }
}
