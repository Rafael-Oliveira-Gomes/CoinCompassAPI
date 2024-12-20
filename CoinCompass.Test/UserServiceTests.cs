using CoinCompassAPI.Application.DTOs.User;
using CoinCompassAPI.Application.Service;
using CoinCompassAPI.Domain.Entities;
using CoinCompassAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CoinCompass.Test
{
    public class UserServiceTests
    {
        private readonly Mock<UserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<UserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object, new Mock<IHttpContextAccessor>().Object, new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object, null, null, null, null);
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _userService = new UserService(
                _userRepositoryMock.Object,
                _configurationMock.Object,
                _signInManagerMock.Object,
                _userManagerMock.Object,
                _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task ListUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<ApplicationUser> { new ApplicationUser { UserName = "TestUser1" }, new ApplicationUser { UserName = "TestUser2" } };
            _userRepositoryMock.Setup(repo => repo.ListUsers()).ReturnsAsync(users);

            // Act
            var result = await _userService.ListUsers();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("TestUser1", result[0].UserName);
            Assert.Equal("TestUser2", result[1].UserName);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = "1";
            var user = new ApplicationUser { Id = userId, UserName = "TestUser" };
            _userRepositoryMock.Setup(repo => repo.GetUser(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.Equal(userId, result.Id);
            Assert.Equal("TestUser", result.UserName);
        }

        [Fact]
        public async Task GetUserById_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _userRepositoryMock.Setup(repo => repo.GetUser(userId)).ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserById(userId));
        }

        [Fact]
        public async Task SignUp_ShouldReturnTrue_WhenUserIsCreated()
        {
            // Arrange
            var signUpDto = new SignUpDTO { Username = "newuser", Email = "newuser@example.com", Password = "Password123" };
            _userManagerMock.Setup(um => um.FindByNameAsync(signUpDto.Username)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.FindByEmailAsync(signUpDto.Email)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), signUpDto.Password)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.SignUp(signUpDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SignIn_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var signInDto = new SignInDTO { Username = "testuser", Password = "Password123" };
            var user = new ApplicationUser { UserName = signInDto.Username };
            var roles = new List<string> { "User" };

            _userManagerMock.Setup(um => um.FindByNameAsync(signInDto.Username)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, signInDto.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
            _configurationMock.Setup(config => config["JWT:Secret"]).Returns("sua_chave_secreta");
            _configurationMock.Setup(config => config["JWT:ValidIssuer"]).Returns("seu_issuer");
            _configurationMock.Setup(config => config["JWT:ValidAudience"]).Returns("seu_audience");

            // Act
            var result = await _userService.SignIn(signInDto);

            // Assert
            Assert.NotNull(result.access_token);
            Assert.True(result.expiration > DateTime.UtcNow);
        }
    }
}
