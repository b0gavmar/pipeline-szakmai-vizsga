using Moq;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Backend.Services.DeviceServices;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using MockQueryable.Moq;
using MockQueryable;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.ChargingInstanceModels;

namespace PipeLine.Backend.Tests
{
    [TestClass]
    public class DeviceServiceTests
    {
        private readonly Mock<IDeviceRepo> _deviceRepoMock;
        private readonly Mock<IDeviceAssembler> _deviceAssemblerMock;
        private readonly Mock<IChargingInstanceRepo> _chargingInstanceRepoMock;

        private readonly IDeviceService _deviceService;

        public DeviceServiceTests()
        {
            _deviceRepoMock = new Mock<IDeviceRepo>();
            _deviceAssemblerMock = new Mock<IDeviceAssembler>();
            _chargingInstanceRepoMock = new Mock<IChargingInstanceRepo>();

            _deviceService = new DeviceService(
                _deviceRepoMock.Object,
                _deviceAssemblerMock.Object,
                _chargingInstanceRepoMock.Object
            );
        }

        [TestMethod]
        public async Task GetAllDevices_ShouldReturnListOfDevices()
        {
            // Arrange
            var devices = new List<Device>
            {
                new Device { Id = Guid.NewGuid(), Name = "Test Device" }
            };

            _deviceRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(devices.AsQueryable().BuildMock());

            _deviceAssemblerMock
                .Setup(x => x.ToDtos(It.IsAny<List<Device>>()))
                .Returns<List<Device>>(input => input.Select(d => new DeviceDto { Id = d.Id, Name = d.Name }).ToList());


            // Act
            var result = await _deviceService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test Device", result.First().Name);
        }

        [TestMethod]
        public async Task GetDevicesByUserId_ShouldReturnDevicesForSpecificUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var devices = new List<Device>
            {
                new Device { Id = Guid.NewGuid(), Name = "Device 1", ApplicationUserId = userId },
                new Device { Id = Guid.NewGuid(), Name = "Device 2", ApplicationUserId = Guid.NewGuid() }
            };

            var usersMockDevices = devices
                .Where(d => d.ApplicationUserId == userId)
                .AsQueryable()
                .BuildMock();

            _deviceRepoMock
                .Setup(repo => repo.GetDevicesByUserId(userId))
                .Returns(usersMockDevices);

            _deviceAssemblerMock
                .Setup(x => x.ToDtos(It.IsAny<List<Device>>()))
                .Returns<List<Device>>(input => input.Select(d => new DeviceDto { Id = d.Id, Name = d.Name, ApplicationUserId = d.ApplicationUserId }).ToList());

            // Act
            var result = await _deviceService.GetDevicesByUserIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Device 1", result.First().Name);
        }

        [TestMethod]
        public async Task GetFilteredDevicesByUserId_ShouldReturnMatchingDevices()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var devices = new List<Device>
            {
                new Device { Id = Guid.NewGuid(), Name = "Bike A", ApplicationUserId = userId },
                new Device { Id = Guid.NewGuid(), Name = "Scooter B", ApplicationUserId = userId },
                new Device { Id = Guid.NewGuid(), Name = "Bike C", ApplicationUserId = userId },
                new Device { Id = Guid.NewGuid(), Name = "Bike D", ApplicationUserId = Guid.NewGuid() }
            };


            var filteredMockDevices = devices
                .Where(d => d.ApplicationUserId == userId && d.Name.Contains("Bike"))
                .AsQueryable()
                .BuildMock();

            _deviceRepoMock
                .Setup(repo => repo.GetFilteredDevicesByUserId("Bike",userId))
                .Returns(filteredMockDevices);


            _deviceAssemblerMock
                .Setup(x => x.ToDtos(It.IsAny<List<Device>>()))
                .Returns<List<Device>>(input => input.Select(d => new DeviceDto { Id = d.Id, Name = d.Name, ApplicationUserId = d.ApplicationUserId}).ToList());

            // Act
            var result = await _deviceService.GetFilteredDevicesByUserIdAsync("Bike", userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Bike A", result.First().Name);
        }

        [TestMethod]
        public async Task GetMyNonChargingDevices_ShouldReturnDevicesWithoutActiveCharging()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var devices = new List<Device>
            {
                new Device { Id = Guid.NewGuid(), Name = "Idle Device", ApplicationUserId = userId, ChargingInstances = new List<ChargingInstance>() { new ChargingInstance { End = DateTime.Today.AddDays(-1) } } },
                new Device { Id = Guid.NewGuid(), Name = "Active Device", ApplicationUserId = userId, ChargingInstances = new List<ChargingInstance> { new ChargingInstance { End = null } } }
            };

            var nonChargingMockDevices = devices
                .Where(d => d.ApplicationUserId == userId && d.ChargingInstances.All(ci => ci.End == null))
                .AsQueryable()
                .BuildMock();

            _deviceRepoMock
                .Setup(repo => repo.GetNonChargingDevicesByUserId(userId))
                .Returns(nonChargingMockDevices);

            _deviceAssemblerMock
                .Setup(x => x.ToDtos(It.IsAny<List<Device>>()))
                .Returns<List<Device>>(input => input.Select(d => new DeviceDto { Id = d.Id, Name = d.Name }).ToList());

            // Act
            var result = await _deviceService.GetNonChargingDevicesByUserId(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Active Device", result.First().Name);
        }

        [TestMethod]
        public async Task GetNumberOfDevicesByUserId_ShouldReturnCorrectCount()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _deviceRepoMock
                .Setup(repo => repo.GetNumberOfDevicesByUserIdAsync(userId))
                .ReturnsAsync(3);

            // Act
            var result = await _deviceService.GetNumberOfDevicesByUserIdAsync(userId);

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public async Task CanDelete_ShouldReturnTrue_WhenNoActiveChargingInstances()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var chargingInstances = new List<ChargingInstance>
            {
                new ChargingInstance { DeviceId = deviceId, End = DateTime.Now.AddDays(-1) }
            };

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(chargingInstances.AsQueryable().BuildMock());

            // Act
            var result = await _deviceService.CanDelete(deviceId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CanDelete_ShouldReturnFalse_WhenActiveChargingInstancesExist()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var chargingInstances = new List<ChargingInstance>
            {
                new ChargingInstance { DeviceId = deviceId, End = null }
            };

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(chargingInstances.AsQueryable().BuildMock());

            // Act
            var result = await _deviceService.CanDelete(deviceId);

            // Assert
            Assert.IsFalse(result);
        }


    }
}
