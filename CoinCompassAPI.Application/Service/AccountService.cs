using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Infrastructure.Interface;

namespace CoinCompassAPI.Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public Task<bool> AtualizarAccount(int id, CreateAccountDto AccountDto)
        {
            throw new NotImplementedException();
        }

        public Task CadastrarAccount(CreateAccountDto AccountDto)
        {
            throw new NotImplementedException();
        }   

        public Task<IEnumerable<CreateAccountDto>> ConsultarAccount(int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public Task<CreateAccountDto> ConsultarAccountPorID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarAccount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
