using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Identity;
using PipeLine.Backend.Services.Authentication;
using PipeLine.HttpService.Dtos;
using PipeLine.Shared.Models.UserModels;
using System;
using System.Threading.Tasks;
using PipeLine.Shared.Dtos.UserDtos;

namespace PipeLine.Backend.Tests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IJwtService> _jwtServiceMock;

        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            _jwtServiceMock = new Mock<IJwtService>();

            _authenticationService = new AuthenticationService(
                _userManagerMock.Object,
                _jwtServiceMock.Object
            );
        }

        [TestMethod]
        public async Task LoginAsync_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            var request = new LoginRegisterRequest { Email = "test@example.com", Password = "password", StayLoggedIn = true };
            var user = new ApplicationUser { Id = Guid.NewGuid(), Email = request.Email };

            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email.ToUpper())).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, request.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.IsInRoleAsync(user, "User")).ReturnsAsync(true);
            _jwtServiceMock.Setup(x => x.GenerateToken(user.Id.ToString(), user.Email, request.StayLoggedIn)).Returns("mocked_token");

            var result = await _authenticationService.LoginAsync(request);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("mocked_token", result.Token);
            Assert.AreEqual("Login successful", result.Message);
        }

        [TestMethod]
        public async Task LoginAsync_ShouldFail_WhenUserNotFound()
        {
            var request = new LoginRegisterRequest { Email = "notfound@example.com", Password = "password" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email.ToUpper())).ReturnsAsync((ApplicationUser)null);

            var result = await _authenticationService.LoginAsync(request);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("The email and password do not match", result.ErrorMessage);
        }

        [TestMethod]
        public async Task RegisterAsync_ShouldReturnSuccess_WhenDataIsValid()
        {
            var request = new LoginRegisterRequest { Email = "newuser@example.com", Password = "password", StayLoggedIn = true };
            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email.ToUpper())).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), request.Password)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User")).ReturnsAsync(IdentityResult.Success);
            _jwtServiceMock.Setup(x => x.GenerateToken(It.IsAny<string>(), request.Email, request.StayLoggedIn)).Returns("mocked_token");

            var result = await _authenticationService.RegisterAsync(request);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("mocked_token", result.Token);
            Assert.AreEqual("Registration successful", result.Message);
        }

        [TestMethod]
        public async Task RegisterAsync_ShouldFail_WhenEmailAlreadyExists()
        {
            var request = new LoginRegisterRequest { Email = "existing@example.com", Password = "password" };
            var existingUser = new ApplicationUser { Email = request.Email };

            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email.ToUpper())).ReturnsAsync(existingUser);

            var result = await _authenticationService.RegisterAsync(request);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Email is already in use.", result.ErrorMessage);
        }

        [TestMethod]
        public async Task ChangePasswordAsync_ShouldReturnSuccess_WhenDataIsValid()
        {
            var request = new ChangePasswordRequest { Email = "user@example.com", CurrentPassword = "oldpass", NewPassword = "newpass" };
            var user = new ApplicationUser { Email = request.Email };

            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email.ToUpper())).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword)).ReturnsAsync(IdentityResult.Success);

            var result = await _authenticationService.ChangePasswordAsync(request);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Password changed successfully", result.Message);
        }

        [TestMethod]
        public async Task ChangePasswordAsync_ShouldFail_WhenUserNotFound()
        {
            var request = new ChangePasswordRequest { Email = "notfound@example.com", CurrentPassword = "oldpass", NewPassword = "newpass" };
            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email.ToUpper())).ReturnsAsync((ApplicationUser)null);

            var result = await _authenticationService.ChangePasswordAsync(request);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("User not found.", result.ErrorMessage);
        }
    }
}
