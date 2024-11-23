using CoinCompassAPI.Application.DTOs.Investment;

namespace CoinCompassAPI.Application.Interface
{
    public interface IInvestmentService
    {
        Task CadastrarInvestment(CreateInvestmentDto InvestmentDto);
        Task<IEnumerable<CreateInvestmentDto>> ConsultarInvestment(int skip = 0, int take = 20);
        Task<CreateInvestmentDto> ConsultarInvestmentPorID(int id);
        Task<bool> AtualizarInvestment(int id, CreateInvestmentDto InvestmentDto);
        Task<bool> DeletarInvestment(int id);
    }
}
