using CoinCompassAPI.Application.DTOs.OutGoigns;
using CoinCompassAPI.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinCompassAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OutgoingsController : ControllerBase
    {
        private readonly IOutgoingsService _outgoingsService;
        private readonly ILogger<OutgoingsController> _logger;

        public OutgoingsController(IOutgoingsService outgoingsService, ILogger<OutgoingsController> logger)
        {
            _outgoingsService = outgoingsService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOutgoings([FromBody] CreateOutgoingsDto outgoingsDto)
        {
            try
            {
                await _outgoingsService.CriarOutGoings(outgoingsDto);
                return Created("", new { message = "Despesa criada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao criar a despesa.", detalhe = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOutgoings([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var outgoings = await _outgoingsService.ConsultarOutGoings(skip, take);
                return Ok(outgoings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar despesas: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar as despesas.", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOutgoingById(int id)
        {
            try
            {
                var outgoing = await _outgoingsService.ConsultarOutGoingsPorID(id);
                return Ok(outgoing);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao consultar despesa: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar a despesa.", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOutgoing(int id, [FromBody] CreateOutgoingsDto outgoingsDto)
        {
            try
            {
                var sucesso = await _outgoingsService.AtualizarOutGoings(id, outgoingsDto);
                if (!sucesso) return NotFound(new { message = "Despesa não encontrada para atualização." });

                return Ok(new { message = "Despesa atualizada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao atualizar a despesa.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutgoing(int id)
        {
            try
            {
                var sucesso = await _outgoingsService.DeletarOutGoings(id);
                if (!sucesso) return NotFound(new { message = "Despesa não encontrada para exclusão." });

                return Ok(new { message = "Despesa deletada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao deletar despesa: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar a despesa.", detalhe = ex.Message });
            }
        }
    }
}
