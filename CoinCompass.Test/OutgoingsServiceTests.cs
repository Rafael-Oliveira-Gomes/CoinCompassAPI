//using CoinCompassAPI.Application.DTOs.OutGoigns;
//using CoinCompassAPI.Application.Service;
//using CoinCompassAPI.Domain.Entities;
//using CoinCompassAPI.Infrastructure.Interface;
//using Moq;

//namespace CoinCompass.Test
//{
//    public class OutgoingsServiceTests
//    {
//        private readonly Mock<IOutgoingRepository> _outgoingsRepositoryMock;
//        private readonly OutgoingsService _outgoingsService;

//        public OutgoingsServiceTests()
//        {
//            _outgoingsRepositoryMock = new Mock<IOutgoingRepository>();
//            _outgoingsService = new OutgoingsService(_outgoingsRepositoryMock.Object);
//        }

//        [Fact]
//        public async Task CriarOutgoings_ShouldAddOutgoings()
//        {
//            // Arrange
//            var outgoingsDto = new CreateOutgoingsDto
//            {
//                UserId = 1,
//                TipoDespesa = "Aluguel",
//                Data = DateTime.Now,
//                Descricao = "Pagamento mensal",
//                FormaPagamento = "Cartão",
//                ValorDespesa = 1500.00m
//            };

//            // Act
//            await _outgoingsService.CriarOutGoings(outgoingsDto);

//            // Assert
//            _outgoingsRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Outgoings>()), Times.Once);
//        }

//        [Fact]
//        public async Task AtualizarOutgoings_ShouldUpdateOutgoings_WhenOutgoingsExists()
//        {
//            // Arrange
//            var existingOutgoings = new Outgoings(1, "Aluguel", DateTime.Now, "Pagamento mensal", "Cartão", 1500.00m);
//            _outgoingsRepositoryMock.Setup(r => r.ConsultarGastosPorID(It.IsAny<int>())).ReturnsAsync(existingOutgoings);

//            var outgoingsDto = new CreateOutgoingsDto
//            {
//                UserId = 1,
//                TipoDespesa = "Aluguel",
//                Data = DateTime.Now,
//                Descricao = "Pagamento mensal atualizado",
//                FormaPagamento = "Cartão",
//                ValorDespesa = 1550.00m
//            };

//            // Act
//            var result = await _outgoingsService.AtualizarOutGoings(1, outgoingsDto);

//            // Assert
//            Assert.True(result);
//            _outgoingsRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Outgoings>()), Times.Once);
//        }

//        [Fact]
//        public async Task ConsultarOutgoingsPorID_ShouldReturnOutgoingsDto_WhenOutgoingsExists()
//        {
//            // Arrange
//            var existingOutgoings = new Outgoings(1, "Aluguel", DateTime.Now, "Pagamento mensal", "Cartão", 1500.00m);
//            _outgoingsRepositoryMock.Setup(r => r.ConsultarGastosPorID(It.IsAny<int>())).ReturnsAsync(existingOutgoings);

//            // Act
//            var result = await _outgoingsService.ConsultarOutGoingsPorID(1);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(existingOutgoings.UserId, result.UserId);
//            Assert.Equal(existingOutgoings.Description, result.Descricao);
//            Assert.Equal(existingOutgoings.Date, result.Data);
//            Assert.Equal(existingOutgoings.AmountOutGoings, result.ValorDespesa);
//            Assert.Equal(existingOutgoings.TypeOutgoings, result.FormaPagamento);
//            Assert.Equal(existingOutgoings.HowPaid, result.TipoDespesa);
//        }

//        [Fact]
//        public async Task DeletarOutgoings_ShouldDeleteOutgoings_WhenOutgoingsExists()
//        {
//            // Arrange
//            var existingOutgoings = new Outgoings(1, "Aluguel", DateTime.Now, "Pagamento mensal", "Cartão", 1500.00m);
//            _outgoingsRepositoryMock.Setup(r => r.ConsultarGastosPorID(It.IsAny<int>())).ReturnsAsync(existingOutgoings);

//            // Act
//            var result = await _outgoingsService.DeletarOutGoings(1);

//            // Assert
//            Assert.True(result);
//            _outgoingsRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Outgoings>()), Times.Once);
//        }

//        [Fact]
//        public async Task ConsultarOutgoingsPorID_ShouldThrowException_WhenOutgoingsNotExists()
//        {
//            // Arrange
//            _outgoingsRepositoryMock.Setup(r => r.ConsultarGastosPorID(It.IsAny<int>())).ReturnsAsync((Outgoings)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<Exception>(() => _outgoingsService.ConsultarOutGoingsPorID(1));
//        }
//    }
//}
