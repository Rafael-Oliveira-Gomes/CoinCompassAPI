//using CoinCompassAPI.Application.DTOs.Account;
//using CoinCompassAPI.Application.Service;
//using CoinCompassAPI.Domain.Entities;
//using CoinCompassAPI.Infrastructure.Interface;
//using Moq;

//namespace CoinCompass.Test
//{
//    public class AccountServiceTests
//    {
//        private readonly Mock<IAccountRepository> _accountRepositoryMock;
//        private readonly AccountService _accountService;

//        public AccountServiceTests()
//        {
//            _accountRepositoryMock = new Mock<IAccountRepository>();
//            _accountService = new AccountService(_accountRepositoryMock.Object);
//        }

//        [Fact]
//        public async Task CriarAccount_ShouldAddAccount()
//        {
//            // Arrange
//            var accountDto = new CreateAccountDto
//            {
//                UsuarioId = 1,
//                TipoConta = "Corrente",
//                Saldo = 1000,
//                NomeBanco = "Banco Teste"
//            };

//            // Act
//            await _accountService.CriarAccount(accountDto);

//            // Assert
//            _accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Once);
//        }

//        [Fact]
//        public async Task AtualizarAccount_ShouldThrowException_WhenAccountDoesNotExist()
//        {
//            // Arrange
//            var accountDto = new CreateAccountDto
//            {
//                UsuarioId = 1,
//                TipoConta = "Corrente",
//                Saldo = 1000,
//                NomeBanco = "Banco Teste"
//            };

//            _accountRepositoryMock
//                .Setup(x => x.ConsultarContaoPorID(It.IsAny<int>()))
//                .ReturnsAsync((Account)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<Exception>(() => _accountService.AtualizarAccount(1, accountDto));
//        }

//        [Fact]
//        public async Task AtualizarAccount_ShouldUpdateAccount()
//        {
//            // Arrange
//            var account = new Account(1, "Corrente", 1000, "Banco Antigo");
//            var accountDto = new CreateAccountDto
//            {
//                UsuarioId = 1,
//                TipoConta = "Poupança",
//                Saldo = 2000,
//                NomeBanco = "Banco Atualizado"
//            };

//            _accountRepositoryMock
//                .Setup(x => x.ConsultarContaoPorID(It.IsAny<int>()))
//                .ReturnsAsync(account);

//            // Act
//            var result = await _accountService.AtualizarAccount(1, accountDto);

//            // Assert
//            Assert.True(result);
//            Assert.Equal("Poupança", account.AccountType);
//            Assert.Equal(2000, account.Balance);
//            Assert.Equal("Banco Atualizado", account.BankName);
//        }

//        [Fact]
//        public async Task ConsultarAccountPorID_ShouldReturnAccount()
//        {
//            // Arrange
//            var account = new Account(1, "Corrente", 1000, "Banco Teste");
//            _accountRepositoryMock
//                .Setup(x => x.ConsultarContaoPorID(It.IsAny<int>()))
//                .ReturnsAsync(account);

//            // Act
//            var result = await _accountService.ConsultarAccountPorID(1);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(1, result.UsuarioId);
//            Assert.Equal("Corrente", result.TipoConta);
//            Assert.Equal(1000, result.Saldo);
//            Assert.Equal("Banco Teste", result.NomeBanco);
//        }

//        [Fact]
//        public async Task DeletarAccount_ShouldDeleteAccount()
//        {
//            // Arrange
//            var account = new Account(1, "Corrente", 1000, "Banco Teste");
//            _accountRepositoryMock
//                .Setup(x => x.ConsultarContaoPorID(It.IsAny<int>()))
//                .ReturnsAsync(account);

//            // Act
//            var result = await _accountService.DeletarAccount(1);

//            // Assert
//            Assert.True(result);
//            _accountRepositoryMock.Verify(x => x.DeleteAsync(account), Times.Once);
//        }

//        [Fact]
//        public async Task DeletarAccount_ShouldThrowException_WhenAccountDoesNotExist()
//        {
//            // Arrange
//            _accountRepositoryMock
//                .Setup(x => x.ConsultarContaoPorID(It.IsAny<int>()))
//                .ReturnsAsync((Account)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<Exception>(() => _accountService.DeletarAccount(1));
//        }
//    }
//}
