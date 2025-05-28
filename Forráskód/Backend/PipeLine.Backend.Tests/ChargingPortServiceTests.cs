using MockQueryable;
using Moq;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Services.ChargingStationServices.ChargingPortServices;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;


namespace PipeLine.Backend.Tests
{
    [TestClass]
    public class ChargingPortServiceTests
    {
        private readonly Mock<IChargingInstanceRepo> _chargingInstanceRepoMock;
        private readonly Mock<IChargingPortRepo> _chargingPortRepoMock;
        private readonly Mock<IChargingPortAssembler> _chargingPortAssemblerMock;

        private readonly IChargingPortService _chargingPortService;

        public ChargingPortServiceTests()
        {
            _chargingInstanceRepoMock = new Mock<IChargingInstanceRepo>();
            _chargingPortRepoMock = new Mock<IChargingPortRepo>();
            _chargingPortAssemblerMock = new Mock<IChargingPortAssembler>();

            _chargingPortService = new ChargingPortService(
                _chargingPortRepoMock.Object,
                _chargingPortAssemblerMock.Object,
                _chargingInstanceRepoMock.Object
            );
        }

        [TestMethod]
        public async Task GetNumberOfPortsOfChargingStationAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            var stationId = Guid.NewGuid();

            var ports = new List<ChargingPort>
            {
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = stationId },
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = stationId },
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = stationId }
            };

            _chargingPortRepoMock
                .Setup(repo => repo.GetNumberOfPortsOfChargingStationAsync(stationId))
                .ReturnsAsync(ports.Count);

            // Act
            var result = await _chargingPortService.GetNumberOfPortsOfChargingStationAsync(stationId);

            // Assert
            Assert.AreEqual(ports.Count, result);
        }


        [TestMethod]
        public async Task CanDelete_ShouldReturnTrue_WhenPortIsDisabledAndNoOngoingChargingInstance()
        {
            // Arrange
            var portId = Guid.NewGuid();

            _chargingPortRepoMock
                .Setup(repo => repo.GetByIdAsync(portId))
                .ReturnsAsync(new ChargingPort { Id = portId, IsDisabled = true });

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<ChargingInstance>().AsQueryable().BuildMock());

            // Act
            var result = await _chargingPortService.CanDelete(portId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CanDelete_ShouldReturnFalse_WhenOngoingChargingInstanceExists()
        {
            // Arrange
            var portId = Guid.NewGuid();

            _chargingPortRepoMock
                .Setup(repo => repo.GetByIdAsync(portId))
                .ReturnsAsync(new ChargingPort { Id = portId, IsDisabled = true });

            _chargingInstanceRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<ChargingInstance>
                            {
                                new ChargingInstance { ChargingPortId = portId, End = null }
                            }
                            .AsQueryable()
                            .BuildMock());

            // Act
            var result = await _chargingPortService.CanDelete(portId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task SetPortToIsChargingFalseByIdAsync_ShouldChangePortStateAndReturnSuccess()
        {
            // Arrange
            var portId = Guid.NewGuid();
            var port = new ChargingPort { Id = portId, IsCharging = true };

            _chargingPortRepoMock
                .Setup(repo => repo.SetPortToIsChargingFalseByIdAsync(portId))
                .Callback(() => port.IsCharging = false)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _chargingPortService.SetPortToIsChargingFalseByIdAsync(portId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(port.IsCharging);
        }

        [TestMethod]
        public async Task SetPortToIsChargingByIdAsync_ShouldChangePortStateAndReturnSuccess()
        {
            // Arrange
            var portId = Guid.NewGuid();
            var port = new ChargingPort { Id = portId, IsCharging = false };

            _chargingPortRepoMock
                .Setup(repo => repo.SetPortToIsChargingByIdAsync(portId))
                .Callback(() => port.IsCharging = true)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _chargingPortService.SetPortToIsChargingByIdAsync(portId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(port.IsCharging);
        }

        [TestMethod]
        public async Task SetPortToIsBeingUsedFalseByIdAsync_ShouldChangePortStateAndReturnSuccess()
        {
            // Arrange
            var portId = Guid.NewGuid();
            var port = new ChargingPort { Id = portId, IsBeingUsed = true };

            _chargingPortRepoMock
                .Setup(repo => repo.SetPortToIsBeingUsedFalseByIdAsync(portId))
                .Callback(() => port.IsBeingUsed = false)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _chargingPortService.SetPortToIsBeingUsedFalseByIdAsync(portId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(port.IsBeingUsed);
        }

        [TestMethod]
        public async Task SetPortToIsBeingUsedByIdAsync_ShouldChangePortStateAndReturnSuccess()
        {
            // Arrange
            var portId = Guid.NewGuid();
            var port = new ChargingPort { Id = portId, IsBeingUsed = false };

            _chargingPortRepoMock
                .Setup(repo => repo.SetPortToIsBeingUsedByIdAsync(portId))
                .Callback(() => port.IsBeingUsed = true)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _chargingPortService.SetPortToIsBeingUsedByIdAsync(portId);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(port.IsBeingUsed);
        }

        [TestMethod]
        public async Task GetFilteredPortsByChargingStationIdAsync_ShouldReturnFilteredPorts()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var ports = new List<ChargingPort>
            {
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = stationId },
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = stationId },
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = Guid.NewGuid() }
            };

            var filteredPorts = ports.Where(p => p.ChargingStationId == stationId);

            _chargingPortRepoMock
                .Setup(repo => repo.GetFilteredPortsByChargingStationIdAsync(null, stationId))
                .ReturnsAsync(filteredPorts);

            _chargingPortAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingPort>>()))
                .Returns<List<ChargingPort>>(input => input.Select(p => new ChargingPortDto { Id = p.Id }).ToList());

            // Act
            var result = await _chargingPortService.GetFilteredPortsByChargingStationIdAsync(null, stationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetFilteredPortsByChargingStationIdAsync_ShouldReturnNoPorts()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var ports = new List<ChargingPort>
            {
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = stationId },
                new ChargingPort { Id = Guid.NewGuid(), ChargingStationId = stationId }
            };

            var filteredPorts = ports.Where(p => p.ChargingStationId == Guid.NewGuid());

            _chargingPortRepoMock
                .Setup(repo => repo.GetFilteredPortsByChargingStationIdAsync(null, stationId))
                .ReturnsAsync(filteredPorts);

            _chargingPortAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingPort>>()))
                .Returns<List<ChargingPort>>(input => input.Select(p => new ChargingPortDto { Id = p.Id }).ToList());

            // Act
            var result = await _chargingPortService.GetFilteredPortsByChargingStationIdAsync(null, stationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

    }
}