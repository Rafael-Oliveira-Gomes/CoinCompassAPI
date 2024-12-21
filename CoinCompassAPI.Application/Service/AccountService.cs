using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.Interface;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;

namespace CoinCompassAPI.Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserService _userService;
        public AccountService(IAccountRepository accountRepository, IUserService userService)
        {
            _accountRepository = accountRepository;
            _userService = userService;
        }

        public async Task<bool> AtualizarAccount(int id, CreateAccountDto AccountDto)
        {
            var conta = await _accountRepository.ConsultarContaoPorID(id);
            if (conta == null)
            {
                throw new Exception("Conta não encontrado para atualizar.");
            }

            conta.Update(AccountDto.TipoConta, AccountDto.Saldo, AccountDto.NomeBanco);

            return true;
        }

        public async Task CriarAccount(CreateAccountDto AccountDto)
        {
            var currentUser = await _userService.GetCurrentUser();
            var conta = new Account(currentUser.Id, AccountDto.TipoConta, AccountDto.Saldo, AccountDto.NomeBanco);
            await _accountRepository.AddAsync(conta);
        }   

        public Task<IEnumerable<CreateAccountDto>> ConsultarAccount(int skip = 0, int take = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateAccountDto> ConsultarAccountPorID(int id)
        {
            var conta = await _accountRepository.ConsultarContaoPorID(id);
            if (conta == null)
            {
                throw new Exception("Conta não encontrado para consultar!");
            }

            return new CreateAccountDto
            {
                
                TipoConta = conta.AccountType,
                Saldo = conta.Balance,
                NomeBanco = conta.BankName,

            };
        }

        public async Task<bool> DeletarAccount(int id)
        {
            var conta = await _accountRepository.ConsultarContaoPorID(id);
            if (conta == null)
            {
                throw new Exception("Conta não encontrado para consultar!");
            }

            await _accountRepository.DeleteAsync(conta);

            return true;
        }
    }
}
