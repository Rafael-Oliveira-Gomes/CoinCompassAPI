using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.DTOs.Investment;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using CoinCompassAPI.Infrastructure.Repository;

namespace CoinCompassAPI.Application.Service
{
    public class InvestmentService : IInvestmentService
    {
        public readonly IInvestimentoRepository _investimentoRepository;
        public InvestmentService(IInvestimentoRepository investimentoRepository)
        {
            _investimentoRepository = investimentoRepository;
        }

        public async Task<bool> AtualizarInvestment(int id, CreateInvestmentDto InvestmentDto)
        {
            var investimento = await _investimentoRepository.ConsultarInvestimentoPorID(id);
            if (investimento == null)
            {
                throw new Exception("investimento não encontrado para atualizar.");
            }

            investimento.Update(InvestmentDto.UsuarioId, InvestmentDto.TipoInvestimento, InvestmentDto.Quantia, InvestmentDto.DataInicio, InvestmentDto.DataFim);

            return true;
        }

        public async Task CadastrarInvestment(CreateInvestmentDto InvestmentDto)
        {
            var investimento = new Investment(InvestmentDto.UsuarioId, InvestmentDto.TipoInvestimento, InvestmentDto.Quantia, InvestmentDto.DataInicio, InvestmentDto.DataFim);
            await _investimentoRepository.AddAsync(investimento);
        }

        public Task<IEnumerable<CreateInvestmentDto>> ConsultarInvestment(int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateInvestmentDto> ConsultarInvestmentPorID(int id)
        {
            var investimento = await _investimentoRepository.ConsultarInvestimentoPorID(id);
            if (investimento == null)
            {
                throw new Exception("investimento não encontrado para consultar!");
            }

            return new CreateInvestmentDto
            {
                
                UsuarioId = investimento.UserId,
                TipoInvestimento = investimento.InvestmentType,
                Quantia = investimento.Amount,
                TaxaJuros = investimento.InterestRate,
                DataInicio = investimento.StartDate,
                DataFim = investimento.EndDate

            };
        }

        public async Task<bool> DeletarInvestment(int id)
        {
            var investimento = await _investimentoRepository.ConsultarInvestimentoPorID(id);
            if (investimento == null)
            {
                throw new Exception("investimento não encontrado para consultar!");
            }

            await _investimentoRepository.DeleteAsync(investimento);

            return true;
        }
    }
}
