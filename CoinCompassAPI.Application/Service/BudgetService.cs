using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.DTOs.Budget;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Repository;

namespace CoinCompassAPI.Application.Service
{
    public class BudgetService : IBudgetService
    {
        public readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        public async Task<bool> AtualizarBudget(int id, CreateBudgetDto BudgetDto)
        {
            var orcamento = await _budgetRepository.ConsultarOrcamentoPorID(id);
            if (orcamento == null)
            {
                throw new Exception("orcamento não encontrado para atualizar.");
            }

            orcamento.Update(BudgetDto.UsuarioId, BudgetDto.Categoria, BudgetDto.Quantia, BudgetDto.DataInicio, BudgetDto.DataFim);

            return true;
        }

        public async Task CadastrarBudget(CreateBudgetDto BudgetDto)
        {
            var orcamento = new Budget(BudgetDto.UsuarioId, BudgetDto.Categoria, BudgetDto.Quantia, BudgetDto.DataInicio, BudgetDto.DataFim);
            await _budgetRepository.AddAsync(orcamento);
        }

        public Task<IEnumerable<CreateBudgetDto>> ConsultarBudget(int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateBudgetDto> ConsultarBudgetPorID(int id)
        {
            var orcamento = await _budgetRepository.ConsultarOrcamentoPorID(id);
            if (orcamento == null)
            {
                throw new Exception("Orçamento não encontrado para consultar!");
            }

            return new CreateBudgetDto
            {
                UsuarioId = orcamento.UserId,
                Categoria = orcamento.Category,
                Quantia = orcamento.Amount,
                DataInicio = orcamento.StartDate,
                DataFim = orcamento.EndDate
            };
        }

        public async Task<bool> DeletarBudget(int id)
        {
            var orcamento = await _budgetRepository.ConsultarOrcamentoPorID(id);
            if (orcamento == null)
            {
                throw new Exception("Orcamento não encontrado para consultar!");
            }

            await _budgetRepository.DeleteAsync(orcamento);

            return true;
        }
    }
}
