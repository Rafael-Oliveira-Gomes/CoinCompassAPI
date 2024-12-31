using CoinCompassAPI.Application.DTOs.Budget;

namespace CoinCompassAPI.Application.Interface
{
    public interface IBudgetService
    {
        Task CadastrarBudget(CreateBudgetDto BudgetDto);
        Task<IEnumerable<CreateBudgetDto>> ConsultarBudget(int skip = 0, int take = 20);
        Task<ReadBudgetDto> ConsultarBudgetPorID(int id);
        Task<bool> AtualizarBudget(int id, CreateBudgetDto BudgetDto);
        Task<bool> DeletarBudget(int id);
    }
}
