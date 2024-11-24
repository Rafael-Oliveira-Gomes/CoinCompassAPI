using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.DTOs.SavingsGoal;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Repository;

namespace CoinCompassAPI.Application.Service
{
    public class SavingsGoalService : ISavingsGoalService
    {
        public readonly ISavingsGoalRepository _savingsGoalRepository;
        public async Task<bool> AtualizarSavingsGoal(int id, CreateSavingsGoalDto SavingsGoalDto)
        {
            var metasEconomias = await _savingsGoalRepository.ConsultarMetaEconomiaPorID(id);
            if (metasEconomias == null)
            {
                throw new Exception("metasEconomias não encontrado para atualizar.");
            }

            metasEconomias.Update(SavingsGoalDto.UsuarioId, SavingsGoalDto.NomeMeta, SavingsGoalDto.QuantiaObjetivo, SavingsGoalDto.QuantiaAtual, SavingsGoalDto.DataObjetivo);

            return true;
        }

        public async Task CadastrarSavingsGoal(CreateSavingsGoalDto SavingsGoalDto)
        {
            var metasEconomias = new SavingsGoal(SavingsGoalDto.UsuarioId, SavingsGoalDto.NomeMeta, SavingsGoalDto.QuantiaObjetivo, SavingsGoalDto.QuantiaObjetivo, SavingsGoalDto.DataObjetivo);
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

                UsuarioId = metaEconomica.UserId,
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
