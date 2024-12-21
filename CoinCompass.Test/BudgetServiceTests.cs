//using CoinCompassAPI.Application.Service;
//using CoinCompassAPI.Application.DTOs.Budget;
//using CoinCompassAPI.Infrastructure.Interface;
//using Moq;
//using Xunit;
//using System;
//using System.Threading.Tasks;
//using CoinCompassAPI.Domain.Entities;

//public class BudgetServiceTests
//{
//    private readonly Mock<IBudgetRepository> _mockRepository;
//    private readonly BudgetService _service;

//    public BudgetServiceTests()
//    {
//        _mockRepository = new Mock<IBudgetRepository>();
//        _service = new BudgetService(_mockRepository.Object);
//    }

//    [Fact]
//    public async Task AtualizarBudget_OrcamentoExistente_DeveAtualizar()
//    {
//        // Arrange
//        var budgetDto = new CreateBudgetDto
//        {
//            UsuarioId = 1,
//            Categoria = "Categoria Teste",
//            Quantia = 1000,
//            DataInicio = DateTime.Now,
//            DataFim = DateTime.Now.AddMonths(1)
//        };
//        var existingBudget = new Budget(1, "Categoria Teste", 1000, DateTime.Now, DateTime.Now.AddMonths(1));
//        _mockRepository.Setup(r => r.ConsultarOrcamentoPorID(1)).ReturnsAsync(existingBudget);

//        // Act
//        var result = await _service.AtualizarBudget(1, budgetDto);

//        // Assert
//        Assert.True(result);
//        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Budget>()), Times.Once);
//    }

//    [Fact]
//    public async Task AtualizarBudget_OrcamentoNaoEncontrado_DeveLancarException()
//    {
//        // Arrange
//        var budgetDto = new CreateBudgetDto();
//        _mockRepository.Setup(r => r.ConsultarOrcamentoPorID(1)).ReturnsAsync((Budget)null);

//        // Act & Assert
//        await Assert.ThrowsAsync<Exception>(() => _service.AtualizarBudget(1, budgetDto));
//    }

//    [Fact]
//    public async Task CadastrarBudget_DeveCadastrar()
//    {
//        // Arrange
//        var budgetDto = new CreateBudgetDto
//        {
//            UsuarioId = 1,
//            Categoria = "Categoria Teste",
//            Quantia = 1000,
//            DataInicio = DateTime.Now,
//            DataFim = DateTime.Now.AddMonths(1)
//        };

//        // Act
//        await _service.CadastrarBudget(budgetDto);

//        // Assert
//        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Budget>()), Times.Once);
//    }
//}
