using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PipeLine.Backend.Services.ChargingInstanceServices;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Shared.Models.ChargingInstanceModels;
using MockQueryable.Moq;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using MockQueryable;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Models.DeviceModels;

namespace PipeLine.Backend.Tests
{
    [TestClass]
    public class ChargingInstanceServiceTests
    {
        private readonly Mock<IChargingInstanceRepo> _chargingInstanceRepoMock;
        private readonly Mock<IChargingPortRepo> _chargingPortRepoMock;
        private readonly Mock<IDeviceRepo> _deviceRepoMock;
        private readonly Mock<IChargingInstanceAssembler> _chargingInstanceAssemblerMock;

        private readonly IChargingInstanceService _chargingInstanceService;

        public ChargingInstanceServiceTests()
        {
            _chargingInstanceRepoMock = new Mock<IChargingInstanceRepo>();
            _chargingPortRepoMock = new Mock<IChargingPortRepo>();
            _deviceRepoMock = new Mock<IDeviceRepo>();
            _chargingInstanceAssemblerMock = new Mock<IChargingInstanceAssembler>();

            _chargingInstanceService = new ChargingInstanceService(
                _chargingInstanceRepoMock.Object,
                _chargingPortRepoMock.Object,
                _deviceRepoMock.Object,
                _chargingInstanceAssemblerMock.Object
            );
        }

        [TestMethod]
        public async Task GetAllChargingInstancesAsync_ShouldReturnAllInstances()
        {
            // Arrange
            var instances = new List<ChargingInstance>
            {
                new ChargingInstance { Id = Guid.NewGuid() },
                new ChargingInstance { Id = Guid.NewGuid() }
            };

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(instances.AsQueryable().BuildMock());

            _chargingInstanceAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingInstance>>()))
                .Returns<List<ChargingInstance>>(input => input.Select(ci => new ChargingInstanceDto { Id = ci.Id }).ToList());

            // Act
            var result = await _chargingInstanceService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetAllChargingInstances_ShouldReturnEmptyList_WhenNoInstancesExist()
        {
            // Arrange
            var instances = new List<ChargingInstance>();

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(instances.AsQueryable().BuildMock());

            _chargingInstanceAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingInstance>>()))
                .Returns(new List<ChargingInstanceDto>());

            // Act
            var result = await _chargingInstanceService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetAllChargingInstances_ShouldThrowException_WhenRepoFails()
        {
            // Arrange
            _chargingInstanceRepoMock
                .Setup(repo => repo.GetAll())
                .Throws(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _chargingInstanceService.GetAllAsync());
        }

        [TestMethod]
        public async Task GetFilteredInstancesByUserIdAsync_ShouldReturnMatchingInstances()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var startDate = DateTime.Now.AddDays(-4);
            var endDate = DateTime.Now.AddDays(-2);

            var instances = new List<ChargingInstance>
            {
                new ChargingInstance { Id = Guid.NewGuid(), Start = DateTime.Now.AddDays(-3), End=DateTime.Now.AddDays(-3).AddHours(1) ,Device = new Device { ApplicationUserId = userId, Name = "Bike A", Manufacturer = "Test", Model = "ModelX" } },
                new ChargingInstance { Id = Guid.NewGuid(), Start = DateTime.Now.AddDays(-2), End=DateTime.Now.AddDays(-2).AddHours(1) ,Device = new Device { ApplicationUserId = userId, Name = "Scooter", Manufacturer = "Test", Model = "ModelY" } },
                new ChargingInstance { Id = Guid.NewGuid(), Start = DateTime.Now.AddDays(-1), End=DateTime.Now.AddDays(-1).AddHours(1) ,Device = new Device { ApplicationUserId = Guid.NewGuid(), Name = "Bike C", Manufacturer = "Test", Model = "ModelZ" } }
            };

            var filteredInstances = instances
                    .Where(i => i.Device.ApplicationUserId == userId && i.End <= endDate && i.Start >= startDate)
                    .AsQueryable()
                    .BuildMock();

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetFilteredInstancesByUserIdAsync(startDate, endDate, userId, null))
                .Returns(filteredInstances);

            _chargingInstanceAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingInstance>>()))
                .Returns<List<ChargingInstance>>(input => input.Select(ci => new ChargingInstanceDto { Id = ci.Id }).ToList());

            // Act
            var result = await _chargingInstanceService.GetFilteredInstancesByUserIdAsync(userId, startDate, endDate, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(filteredInstances.First().Id, result.First().Id);
        }

        [TestMethod]
        public async Task GetFilteredChargingInstances_ShouldReturnEmptyList_WhenNoInstancesExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var instances = new List<ChargingInstance>();

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetFilteredInstancesByUserIdAsync(null, null, userId, null))
                .Returns(instances.AsQueryable().BuildMock());

            _chargingInstanceAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingInstance>>()))
                .Returns(new List<ChargingInstanceDto>());

            // Act
            var result = await _chargingInstanceService.GetFilteredInstancesByUserIdAsync(userId, null,null,null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetActiveChargingInstancesByUserIdAsync_ShouldReturnActiveInstances()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var instances = new List<ChargingInstance>
            {
                new ChargingInstance { Id = Guid.NewGuid(), Device = new Device { ApplicationUserId = userId }, End = null },
                new ChargingInstance { Id = Guid.NewGuid(), Device = new Device { ApplicationUserId = userId }, End = DateTime.UtcNow }
            };


            var filteredInstances = instances
                    .Where(i => i.Device.ApplicationUserId == userId && i.End == null)
                    .AsQueryable()
                    .BuildMock();

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetFilteredInstancesByUserIdAsync(null, null, userId, false))
                .Returns(filteredInstances);

            _chargingInstanceAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingInstance>>()))
                .Returns<List<ChargingInstance>>(input => input.Select(ci => new ChargingInstanceDto { Id = ci.Id }).ToList());

            // Act
            var result = await _chargingInstanceService.GetFilteredInstancesByUserIdAsync(userId, null, null, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetFinishedChargingInstancesByUserIdAsync_ShouldReturnFinishedInstances()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var instances = new List<ChargingInstance>
            {
                new ChargingInstance { Id = Guid.NewGuid(), Device = new Device { ApplicationUserId = userId }, End = null },
                new ChargingInstance { Id = Guid.NewGuid(), Device = new Device { ApplicationUserId = userId }, End = DateTime.UtcNow }
            };

            var filteredInstances = instances
                    .Where(i => i.Device.ApplicationUserId == userId && i.End != null)
                    .AsQueryable()
                    .BuildMock();

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetFilteredInstancesByUserIdAsync(null, null, userId, true))
                .Returns(filteredInstances);

            _chargingInstanceAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingInstance>>()))
                .Returns<List<ChargingInstance>>(input => input.Select(ci => new ChargingInstanceDto { Id = ci.Id }).ToList());

            // Act
            var result = await _chargingInstanceService.GetFilteredInstancesByUserIdAsync(userId,null,null,true);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetFilteredChargingInstances_ShouldThrowException_WhenRepoFails()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetFilteredInstancesByUserIdAsync(null, null, userId, true))
                .Throws(new InvalidOperationException("Database failure"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _chargingInstanceService.GetFilteredInstancesByUserIdAsync(userId, null, null, null));
        }

    }
}