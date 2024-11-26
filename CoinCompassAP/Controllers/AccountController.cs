using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoinCompassAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto accountDto)
        {
            try
            {
                await _accountService.CriarAccount(accountDto);
                return Created("", new { message = "Conta criada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao criar a conta.", detalhe = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            try
            {
                var accounts = await _accountService.ConsultarAccount(skip, take);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar contas: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar as contas.", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var account = await _accountService.ConsultarAccountPorID(id);
                return Ok(account);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao consultar conta: {Message}", ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao consultar a conta.", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] CreateAccountDto accountDto)
        {
            try
            {
                var sucesso = await _accountService.AtualizarAccount(id, accountDto);
                if (!sucesso) return NotFound(new { message = "Conta não encontrada para atualização." });

                return Ok(new { message = "Conta atualizada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro de argumento: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao atualizar a conta.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var sucesso = await _accountService.DeletarAccount(id);
                if (!sucesso) return NotFound(new { message = "Conta não encontrada para exclusão." });

                return Ok(new { message = "Conta deletada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao deletar conta: {Message}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar a conta.", detalhe = ex.Message });
            }
        }
    }
}
