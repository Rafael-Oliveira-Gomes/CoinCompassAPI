using CoinCompassAPI.Application.DTOs.SavingsGoal;
using CoinCompassAPI.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinCompassAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SavingsGoalController : ControllerBase
    {
        private readonly ISavingsGoalService _savingsGoalService;
        private readonly ILogger<SavingsGoalController> _logger;

        public SavingsGoalController(ISavingsGoalService savingsGoalService, ILogger<SavingsGoalController> logger)
        {
            _savingsGoalService = savingsGoalService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSavingsGoal([FromBody] CreateSavingsGoalDto savingsGoalDto)
        {
            try
            {
                await _savingsGoalService.CadastrarSavingsGoal(savingsGoalDto);
                return Created("", new { message = "Meta de economia criada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao criar a meta de economia.", detalhe = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSavingsGoals([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var savingsGoals = await _savingsGoalService.ConsultarSavingsGoal(skip, take);
                return Ok(savingsGoals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar metas de economia: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar as metas de economia.", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSavingsGoalById(int id)
        {
            try
            {
                var savingsGoal = await _savingsGoalService.ConsultarSavingsGoalPorID(id);
                return Ok(savingsGoal);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao consultar meta de economia: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar a meta de economia.", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSavingsGoal(int id, [FromBody] CreateSavingsGoalDto savingsGoalDto)
        {
            try
            {
                var sucesso = await _savingsGoalService.AtualizarSavingsGoal(id, savingsGoalDto);
                if (!sucesso) return NotFound(new { message = "Meta de economia não encontrada para atualização." });

                return Ok(new { message = "Meta de economia atualizada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao atualizar a meta de economia.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSavingsGoal(int id)
        {
            try
            {
                var sucesso = await _savingsGoalService.DeletarSavingsGoal(id);
                if (!sucesso) return NotFound(new { message = "Meta de economia não encontrada para exclusão." });

                return Ok(new { message = "Meta de economia deletada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao deletar meta de economia: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar a meta de economia.", detalhe = ex.Message });
            }
        }
    }
}
