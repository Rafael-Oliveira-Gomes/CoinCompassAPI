using CoinCompassAPI.Application.DTOs.Budget;
using CoinCompassAPI.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinCompassAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        private readonly ILogger<BudgetController> _logger;

        public BudgetController(IBudgetService budgetService, ILogger<BudgetController> logger)
        {
            _budgetService = budgetService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetDto budgetDto)
        {
            try
            {
                await _budgetService.CadastrarBudget(budgetDto);
                return Created("", new { message = "Orçamento criado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao criar o orçamento.", detalhe = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var budgets = await _budgetService.ConsultarBudget(skip, take);
                return Ok(budgets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar orçamentos: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar os orçamentos.", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetById(int id)
        {
            try
            {
                var budget = await _budgetService.ConsultarBudgetPorID(id);
                return Ok(budget);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao consultar orçamento: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar o orçamento.", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(int id, [FromBody] CreateBudgetDto budgetDto)
        {
            try
            {
                var sucesso = await _budgetService.AtualizarBudget(id, budgetDto);
                if (!sucesso) return NotFound(new { message = "Orçamento não encontrado para atualização." });

                return Ok(new { message = "Orçamento atualizado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao atualizar o orçamento.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            try
            {
                var sucesso = await _budgetService.DeletarBudget(id);
                if (!sucesso) return NotFound(new { message = "Orçamento não encontrado para exclusão." });

                return Ok(new { message = "Orçamento deletado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao deletar orçamento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar o orçamento.", detalhe = ex.Message });
            }
        }
    }
}
