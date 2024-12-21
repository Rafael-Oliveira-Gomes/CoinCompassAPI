using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.DTOs.SavingsGoal;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Application.Util;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Repository;

namespace CoinCompassAPI.Application.Service
{
    public class SavingsGoalService : ISavingsGoalService
    {
        public readonly ISavingsGoalRepository _savingsGoalRepository;
        public readonly IBudgetRepository _budgetRepository;
        private readonly ValidacoesFinanceiras _validacoesFinanceiras;
        private readonly IUserService _userService;

        public SavingsGoalService(ISavingsGoalRepository savingsGoalRepository, IBudgetRepository budgetRepository, ValidacoesFinanceiras validacoesFinanceiras, IUserService userService)
        {
            _validacoesFinanceiras = validacoesFinanceiras;
            _savingsGoalRepository = savingsGoalRepository;
            _budgetRepository = budgetRepository;
            _userService = userService;
        }
        public async Task<bool> AtualizarSavingsGoal(int id, CreateSavingsGoalDto SavingsGoalDto)
        {
            var metasEconomias = await _savingsGoalRepository.ConsultarMetaEconomiaPorID(id);
            if (metasEconomias == null)
            {
                throw new Exception("metasEconomias não encontrado para atualizar.");
            }

            metasEconomias.Update(SavingsGoalDto.NomeMeta, SavingsGoalDto.QuantiaObjetivo, SavingsGoalDto.QuantiaAtual, SavingsGoalDto.DataObjetivo);

            return true;
        }

        public async Task CadastrarSavingsGoal(CreateSavingsGoalDto SavingsGoalDto)
        {
            var currentUser = await _userService.GetCurrentUser();

            //ajuste no card #2
            //_validacoesFinanceiras.VerificarOrcamento(SavingsGoalDto.UsuarioId, SavingsGoalDto.QuantiaObjetivo, SavingsGoalDto.DataObjetivo);

            var metasEconomias = new SavingsGoal(currentUser.Id, SavingsGoalDto.NomeMeta, SavingsGoalDto.QuantiaObjetivo, SavingsGoalDto.QuantiaObjetivo, SavingsGoalDto.DataObjetivo);
            await _savingsGoalRepository.AddAsync(metasEconomias);
        }

        public Task<IEnumerable<CreateSavingsGoalDto>> ConsultarSavingsGoal(int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateSavingsGoalDto> ConsultarSavingsGoalPorID(int id)
        {
            var metaEconomica = await _savingsGoalRepository.ConsultarMetaEconomiaPorID(id);
            if (metaEconomica == null)
            {
                throw new Exception("metaEconomica não encontrado para consultar!");
            }

            return new CreateSavingsGoalDto
            {
                NomeMeta = metaEconomica.GoalName,
                DataObjetivo = metaEconomica.TargetDate,
                QuantiaAtual = metaEconomica.CurrentAmount,
                QuantiaObjetivo = metaEconomica.TargetAmount

            };
        }

        public async Task<bool> DeletarSavingsGoal(int id)
        {
            var metaEconomica = await _savingsGoalRepository.ConsultarMetaEconomiaPorID(id);
            if (metaEconomica == null)
            {
                throw new Exception("metaEconomica não encontrado para consultar!");
            }

            await _savingsGoalRepository.DeleteAsync(metaEconomica);

            return true;
        }
    }
}
