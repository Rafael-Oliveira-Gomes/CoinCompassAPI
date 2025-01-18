using CoinCompassAPI.Application.DTOs.Investment;
using CoinCompassAPI.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinCompassAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;
        private readonly ILogger<InvestmentController> _logger;

        public InvestmentController(IInvestmentService investmentService, ILogger<InvestmentController> logger)
        {
            _investmentService = investmentService;
            _logger = logger;
        }

        // Criar novo investimento
        [HttpPost]
        public async Task<IActionResult> CreateInvestment([FromBody] CreateInvestmentDto investmentDto)
        {
            try
            {
                await _investmentService.CadastrarInvestment(investmentDto);
                return Created("", new { message = "Investimento criado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao criar o investimento.", detalhe = ex.Message });
            }
        }

        // Consultar todos os investimentos com paginação
        [HttpGet]
        public async Task<IActionResult> GetInvestments([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var investments = await _investmentService.ConsultarInvestment(skip, take);
                return Ok(investments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar investimentos: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar os investimentos.", detalhe = ex.Message });
            }
        }

        // Consultar investimento por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvestmentById(int id)
        {
            try
            {
                var investment = await _investmentService.ConsultarInvestmentPorID(id);
                return Ok(investment);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao consultar investimento: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar o investimento.", detalhe = ex.Message });
            }
        }

        // Atualizar investimento existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestment(int id, [FromBody] CreateInvestmentDto investmentDto)
        {
            try
            {
                var sucesso = await _investmentService.AtualizarInvestment(id, investmentDto);
                if (!sucesso) return NotFound(new { message = "Investimento não encontrado para atualização." });

                return Ok(new { message = "Investimento atualizado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao atualizar o investimento.", detalhe = ex.Message });
            }
        }

        // Deletar investimento por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestment(int id)
        {
            try
            {
                var sucesso = await _investmentService.DeletarInvestment(id);
                if (!sucesso) return NotFound(new { message = "Investimento não encontrado para exclusão." });

                return Ok(new { message = "Investimento deletado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao deletar investimento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar o investimento.", detalhe = ex.Message });
            }
        }
    }
}
