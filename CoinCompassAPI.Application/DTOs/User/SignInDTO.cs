using System.ComponentModel.DataAnnotations;

namespace CoinCompassAPI.Application.DTOs.User
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
