using CoinCompassAPI.Application.DTOs.Transaction;
using CoinCompassAPI.Application.Service;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using Moq;

namespace CoinCompass.Test
{
    public class TransactionServiceTests
    {
        private readonly Mock<ITransacaoRepository> _transactionRepositoryMock;
        private readonly TransasctionService _transactionService;

        public TransactionServiceTests()
        {
            _transactionRepositoryMock = new Mock<ITransacaoRepository>();
            _transactionService = new TransasctionService(_transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarTransaction_ShouldAddTransaction()
        {
            // Arrange
            var transactionDto = new CreateTransactionDto
            {
                ContaId = 1,
                Tipo = "Depósito",
                Quantia = 1000.00m,
                Data = DateTime.Now,
                Descricao = "Depósito de salário"
            };

            // Act
            await _transactionService.CadastrarTransaction(transactionDto);

            // Assert
            _transactionRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarTransaction_ShouldUpdateTransaction_WhenTransactionExists()
        {
            // Arrange
            var existingTransaction = new Transaction(1, "Depósito", 1000.00m, DateTime.Now, "Depósito de salário");
            _transactionRepositoryMock.Setup(r => r.ConsultarTansacaoPorID(It.IsAny<int>())).ReturnsAsync(existingTransaction);

            var transactionDto = new CreateTransactionDto
            {
                ContaId = 1,
                Tipo = "Depósito",
                Quantia = 1500.00m,
                Data = DateTime.Now,
                Descricao = "Depósito de bônus"
            };

            // Act
            var result = await _transactionService.AtualizarTransaction(1, transactionDto);

            // Assert
            Assert.True(result);
            _transactionRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task ConsultarTransactionPorID_ShouldReturnTransactionDto_WhenTransactionExists()
        {
            // Arrange
            var existingTransaction = new Transaction(1, "Depósito", 1000.00m, DateTime.Now, "Depósito de salário");
            _transactionRepositoryMock.Setup(r => r.ConsultarTansacaoPorID(It.IsAny<int>())).ReturnsAsync(existingTransaction);

            // Act
            var result = await _transactionService.ConsultarTransactionPorID(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingTransaction.AccountId, result.ContaId);
            Assert.Equal(existingTransaction.Type, result.Tipo);
            Assert.Equal(existingTransaction.Amount, result.Quantia);
            Assert.Equal(existingTransaction.Date, result.Data);
            Assert.Equal(existingTransaction.Description, result.Descricao);
        }

        [Fact]
        public async Task DeletarTransaction_ShouldDeleteTransaction_WhenTransactionExists()
        {
            // Arrange
            var existingTransaction = new Transaction(1, "Depósito", 1000.00m, DateTime.Now, "Depósito de salário");
            _transactionRepositoryMock.Setup(r => r.ConsultarTansacaoPorID(It.IsAny<int>())).ReturnsAsync(existingTransaction);

            // Act
            var result = await _transactionService.DeletarTransaction(1);

            // Assert
            Assert.True(result);
            _transactionRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Fact]
        public async Task ConsultarTransactionPorID_ShouldThrowException_WhenTransactionNotExists()
        {
            // Arrange
            _transactionRepositoryMock.Setup(r => r.ConsultarTansacaoPorID(It.IsAny<int>())).ReturnsAsync((Transaction)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _transactionService.ConsultarTransactionPorID(1));
        }
    }
}
