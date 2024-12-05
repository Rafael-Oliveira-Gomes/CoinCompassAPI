using CoinCompassAPI.Application.DTOs.SavingsGoal;
using CoinCompassAPI.Application.Service;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Interface;
using Moq;

namespace CoinCompass.Test
{
    public class SavingsGoalServiceTests
    {
        private readonly Mock<ISavingsGoalRepository> _savingsGoalRepositoryMock;
        private readonly SavingsGoalService _savingsGoalService;

        public SavingsGoalServiceTests()
        {
            _savingsGoalRepositoryMock = new Mock<ISavingsGoalRepository>();
            _savingsGoalService = new SavingsGoalService(_savingsGoalRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarSavingsGoal_ShouldAddSavingsGoal()
        {
            // Arrange
            var savingsGoalDto = new CreateSavingsGoalDto
            {
                UsuarioId = 1,
                NomeMeta = "Viagem",
                QuantiaObjetivo = 5000.00m,
                QuantiaAtual = 1000.00m,
                DataObjetivo = DateTime.Now.AddYears(1)
            };

            // Act
            await _savingsGoalService.CadastrarSavingsGoal(savingsGoalDto);

            // Assert
            _savingsGoalRepositoryMock.Verify(r => r.AddAsync(It.IsAny<SavingsGoal>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarSavingsGoal_ShouldUpdateSavingsGoal_WhenSavingsGoalExists()
        {
            // Arrange
            var existingSavingsGoal = new SavingsGoal(1, "Viagem", 5000.00m, 1000.00m, DateTime.Now.AddYears(1));
            _savingsGoalRepositoryMock.Setup(r => r.ConsultarMetaEconomiaPorID(It.IsAny<int>())).ReturnsAsync(existingSavingsGoal);

            var savingsGoalDto = new CreateSavingsGoalDto
            {
                UsuarioId = 1,
                NomeMeta = "Viagem",
                QuantiaObjetivo = 6000.00m,
                QuantiaAtual = 1500.00m,
                DataObjetivo = DateTime.Now.AddYears(1)
            };

            // Act
            var result = await _savingsGoalService.AtualizarSavingsGoal(1, savingsGoalDto);

            // Assert
            Assert.True(result);
            _savingsGoalRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<SavingsGoal>()), Times.Once);
        }

        [Fact]
        public async Task ConsultarSavingsGoalPorID_ShouldReturnSavingsGoalDto_WhenSavingsGoalExists()
        {
            // Arrange
            var existingSavingsGoal = new SavingsGoal(1, "Viagem", 5000.00m, 1000.00m, DateTime.Now.AddYears(1));
            _savingsGoalRepositoryMock.Setup(r => r.ConsultarMetaEconomiaPorID(It.IsAny<int>())).ReturnsAsync(existingSavingsGoal);

            // Act
            var result = await _savingsGoalService.ConsultarSavingsGoalPorID(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingSavingsGoal.UserId, result.UsuarioId);
            Assert.Equal(existingSavingsGoal.GoalName, result.NomeMeta);
            Assert.Equal(existingSavingsGoal.TargetDate, result.DataObjetivo);
            Assert.Equal(existingSavingsGoal.CurrentAmount, result.QuantiaAtual);
            Assert.Equal(existingSavingsGoal.TargetAmount, result.QuantiaObjetivo);
        }

        [Fact]
        public async Task DeletarSavingsGoal_ShouldDeleteSavingsGoal_WhenSavingsGoalExists()
        {
            // Arrange
            var existingSavingsGoal = new SavingsGoal(1, "Viagem", 5000.00m, 1000.00m, DateTime.Now.AddYears(1));
            _savingsGoalRepositoryMock.Setup(r => r.ConsultarMetaEconomiaPorID(It.IsAny<int>())).ReturnsAsync(existingSavingsGoal);

            // Act
            var result = await _savingsGoalService.DeletarSavingsGoal(1);

            // Assert
            Assert.True(result);
            _savingsGoalRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<SavingsGoal>()), Times.Once);
        }

        [Fact]
        public async Task ConsultarSavingsGoalPorID_ShouldThrowException_WhenSavingsGoalNotExists()
        {
            // Arrange
            _savingsGoalRepositoryMock.Setup(r => r.ConsultarMetaEconomiaPorID(It.IsAny<int>())).ReturnsAsync((SavingsGoal)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _savingsGoalService.ConsultarSavingsGoalPorID(1));
        }
    }
}
