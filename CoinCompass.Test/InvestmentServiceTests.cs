//using CoinCompassAPI.Application.DTOs.Investment;
//using CoinCompassAPI.Application.Service;
//using CoinCompassAPI.Domain.Entities;
//using CoinCompassAPI.Infrastructure.Interface;
//using Moq;

//namespace CoinCompass.Test
//{
//    public class InvestmentServiceTests
//    {
//        private readonly Mock<IInvestimentoRepository> _investimentoRepositoryMock;
//        private readonly InvestmentService _investmentService;

//        public InvestmentServiceTests()
//        {
//            _investimentoRepositoryMock = new Mock<IInvestimentoRepository>();
//            _investmentService = new InvestmentService(_investimentoRepositoryMock.Object);
//        }

//        [Fact]
//        public async Task CadastrarInvestment_ShouldAddInvestment()
//        {
//            // Arrange
//            var investmentDto = new CreateInvestmentDto
//            {
//                UsuarioId = 1,
//                TipoInvestimento = "Ações",
//                Quantia = 1000.00m,
//                DataInicio = DateTime.Now,
//                DataFim = DateTime.Now.AddYears(1)
//            };

//            // Act
//            await _investmentService.CadastrarInvestment(investmentDto);

//            // Assert
//            _investimentoRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Investment>()), Times.Once);
//        }

//        [Fact]
//        public async Task AtualizarInvestment_ShouldUpdateInvestment_WhenInvestmentExists()
//        {
//            // Arrange
//            var existingInvestment = new Investment(1, "Ações", 1000.00m, DateTime.Now, DateTime.Now.AddYears(1));
//            _investimentoRepositoryMock.Setup(r => r.ConsultarInvestimentoPorID(It.IsAny<int>())).ReturnsAsync(existingInvestment);

//            var investmentDto = new CreateInvestmentDto
//            {
//                UsuarioId = 1,
//                TipoInvestimento = "Ações",
//                Quantia = 1500.00m,
//                DataInicio = DateTime.Now,
//                DataFim = DateTime.Now.AddYears(1)
//            };

//            // Act
//            var result = await _investmentService.AtualizarInvestment(1, investmentDto);

//            // Assert
//            Assert.True(result);
//            _investimentoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Investment>()), Times.Once);
//        }

//        [Fact]
//        public async Task ConsultarInvestmentPorID_ShouldReturnInvestmentDto_WhenInvestmentExists()
//        {
//            // Arrange
//            var existingInvestment = new Investment(1, "Ações", 1000.00m, DateTime.Now, DateTime.Now.AddYears(1));
//            _investimentoRepositoryMock.Setup(r => r.ConsultarInvestimentoPorID(It.IsAny<int>())).ReturnsAsync(existingInvestment);

//            // Act
//            var result = await _investmentService.ConsultarInvestmentPorID(1);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(existingInvestment.UserId, result.UsuarioId);
//            Assert.Equal(existingInvestment.InvestmentType, result.TipoInvestimento);
//            Assert.Equal(existingInvestment.Amount, result.Quantia);
//            Assert.Equal(existingInvestment.StartDate, result.DataInicio);
//            Assert.Equal(existingInvestment.EndDate, result.DataFim);
//            Assert.Equal(existingInvestment.InterestRate, result.TaxaJuros);
//        }

//        [Fact]
//        public async Task DeletarInvestment_ShouldDeleteInvestment_WhenInvestmentExists()
//        {
//            // Arrange
//            var existingInvestment = new Investment(1, "Ações", 1000.00m, DateTime.Now, DateTime.Now.AddYears(1));
//            _investimentoRepositoryMock.Setup(r => r.ConsultarInvestimentoPorID(It.IsAny<int>())).ReturnsAsync(existingInvestment);

//            // Act
//            var result = await _investmentService.DeletarInvestment(1);

//            // Assert
//            Assert.True(result);
//            _investimentoRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Investment>()), Times.Once);
//        }

//        [Fact]
//        public async Task ConsultarInvestmentPorID_ShouldThrowException_WhenInvestmentNotExists()
//        {
//            // Arrange
//            _investimentoRepositoryMock.Setup(r => r.ConsultarInvestimentoPorID(It.IsAny<int>())).ReturnsAsync((Investment)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<Exception>(() => _investmentService.ConsultarInvestmentPorID(1));
//        }
//    }
//}
