using CoinCompassAPI.Application.DTOs.User;
using CoinCompassAPI.Application.Service;
using CoinCompassAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinCompassAP.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _authService;

        public AuthController(UserService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            try
            {
                bool ret = await _authService.SignUp(signUpDTO);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn([FromBody] SignInDTO signInDTO)
        {
            try
            {
                SsoDTO ssoDTO = await _authService.SignIn(signInDTO);

                return Ok(ssoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-current-user")]
        public async Task<ActionResult> GetCurrentUser()
        {
            try
            {
                ApplicationUser currentUser = await _authService.GetCurrentUser();

                return Ok(currentUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
