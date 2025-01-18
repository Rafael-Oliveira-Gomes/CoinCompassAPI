using CoinCompassAPI.Application.DTOs.Transaction;
using CoinCompassAPI.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinCompassAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto transactionDto)
        {
            try
            {
                await _transactionService.CadastrarTransaction(transactionDto);
                return Created("", new { message = "Transação criada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao criar a transação.", detalhe = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var transactions = await _transactionService.ConsultarTransaction(skip, take);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar transações: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar as transações.", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var transaction = await _transactionService.ConsultarTransactionPorID(id);
                return Ok(transaction);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao consultar transação: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar a transação.", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] CreateTransactionDto transactionDto)
        {
            try
            {
                var sucesso = await _transactionService.AtualizarTransaction(id, transactionDto);
                if (!sucesso) return NotFound(new { message = "Transação não encontrada para atualização." });

                return Ok(new { message = "Transação atualizada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao atualizar a transação.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                var sucesso = await _transactionService.DeletarTransaction(id);
                if (!sucesso) return NotFound(new { message = "Transação não encontrada para exclusão." });

                return Ok(new { message = "Transação deletada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao deletar transação: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar a transação.", detalhe = ex.Message });
            }
        }
    }
}
