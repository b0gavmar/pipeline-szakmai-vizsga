using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PipeLine.Backend.Assemblers.UserAssemblers;
using PipeLine.Backend.Repos.UserRepos;
using PipeLine.Backend.Services.UserServices;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.UserModels;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MockQueryable;

namespace PipeLine.Backend.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly Mock<IUserRepo> _userRepoMock;
        private readonly Mock<IApplicationUserAssembler> _userAssemblerMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepo>();
            _userAssemblerMock = new Mock<IApplicationUserAssembler>();

            _userService = new UserService(
                _userRepoMock.Object,
                _userAssemblerMock.Object
            );
        }

        [TestMethod]
        public async Task GetAllUsers_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = Guid.NewGuid(), UserName = "user1" },
                new ApplicationUser { Id = Guid.NewGuid(), UserName = "user2" }
            };

            _userRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(users.AsQueryable().BuildMock());

            _userAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ApplicationUser>>()))
                .Returns<List<ApplicationUser>>(input => input.Select(u => new ApplicationUserDto { Id = u.Id, UserName = u.UserName }).ToList());

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(u => u.UserName == "user1"));
        }

        [TestMethod]
        public async Task GetAllUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            _userRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<ApplicationUser>().AsQueryable().BuildMock());

            _userAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ApplicationUser>>()))
                .Returns(new List<ApplicationUserDto>());

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetAllUsers_ShouldThrowException_WhenRepoFails()
        {
            // Arrange
            _userRepoMock
                .Setup(repo => repo.GetAll())
                .Throws(new InvalidOperationException("Database failure"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _userService.GetAllAsync());
        }

        [TestMethod]
        public async Task GetNumberOfUsers_ShouldReturnCorrectCount()
        {
            // Arrange
            _userRepoMock
                .Setup(repo => repo.GetNumberOfUsersAsync())
                .ReturnsAsync(5);

            // Act
            var result = await _userService.GetNumberOfUsersAsync();

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public async Task GetNumberOfUsers_ShouldThrowException_WhenRepoFails()
        {
            // Arrange
            _userRepoMock
                .Setup(repo => repo.GetNumberOfUsersAsync())
                .Throws(new InvalidOperationException("Database failure"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _userService.GetNumberOfUsersAsync());
        }
    }
}
