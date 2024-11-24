using CoinCompassAPI.Application.DTOs.Account;

namespace CoinCompassAPI.Application.Interface
{
    public interface IAccountService
    {
        Task CriarAccount(CreateAccountDto AccountDto);
        Task<IEnumerable<CreateAccountDto>> ConsultarAccount(int skip = 0, int take = 20);
        Task<CreateAccountDto> ConsultarAccountPorID(int id);
        Task<bool> AtualizarAccount(int id, CreateAccountDto AccountDto);
        Task<bool> DeletarAccount(int id);
    }
}
