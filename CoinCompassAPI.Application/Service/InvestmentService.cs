using CoinCompassAPI.Application.DTOs.Investment;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Application.Util;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;

namespace CoinCompassAPI.Application.Service
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestimentoRepository _investimentoRepository;
        private readonly IUserService _userService;
        private readonly ValidacoesFinanceiras _validacoesFinanceiras;
        public InvestmentService(IInvestimentoRepository investimentoRepository, IUserService userService, ValidacoesFinanceiras validacoesFinanceiras)
        {
            _investimentoRepository = investimentoRepository;
            _userService = userService;
            _validacoesFinanceiras = validacoesFinanceiras;
        }

        public async Task<bool> AtualizarInvestment(int id, CreateInvestmentDto InvestmentDto)
        {
            var investimento = await _investimentoRepository.ConsultarInvestimentoPorID(id);
            if (investimento == null)
            {
                throw new Exception("investimento não encontrado para atualizar.");
            }

            investimento.Update(InvestmentDto.TipoInvestimento, InvestmentDto.Quantia, InvestmentDto.DataInicio, InvestmentDto.DataFim);

            return true;
        }

        public async Task CadastrarInvestment(CreateInvestmentDto InvestmentDto)
        {
            var currentUser = await _userService.GetCurrentUser();
            _validacoesFinanceiras.VerificarOrcamento(currentUser.Id,InvestmentDto.Quantia, InvestmentDto.DataFim);
            var investimento = new Investment(currentUser.Id, InvestmentDto.TipoInvestimento, InvestmentDto.Quantia, InvestmentDto.DataInicio, InvestmentDto.DataFim);

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
