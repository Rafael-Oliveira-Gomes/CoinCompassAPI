using CoinCompassAPI.Application.DTOs.Account;
using CoinCompassAPI.Application.DTOs.User;
using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Application.Interface
{
    public interface IUserService
    {
        Task<bool> SignUp(SignUpDTO signUpDTO);
        Task<SsoDTO> SignIn(SignInDTO signInDTO);
        Task<ApplicationUser> GetCurrentUser();
        Task<bool> DeleteUser(string userId);
        void UpdateUser(ApplicationUser user);
        Task<ApplicationUser> GetUserById(string userId);
        Task<List<ApplicationUser>> ListUsers();
    }
}
