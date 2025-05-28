using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PipeLine.Backend.Services.ChargingStationServices;
using PipeLine.Backend.Repos.ChargingStationRepos;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Shared.Models.ChargingStationModels;
using MockQueryable.Moq;
using MockQueryable;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;

namespace PipeLine.Backend.Tests
{
    [TestClass]
    public class ChargingStationServiceTests
    {
        private readonly Mock<IChargingStationRepo> _chargingStationRepoMock;
        private readonly Mock<IChargingStationAssembler> _chargingStationAssemblerMock;
        private readonly Mock<IChargingPortRepo> _chargingPortRepoMock;

        private readonly IChargingStationService _chargingStationService;

        public ChargingStationServiceTests()
        {
            _chargingStationRepoMock = new Mock<IChargingStationRepo>();
            _chargingStationAssemblerMock = new Mock<IChargingStationAssembler>();
            _chargingPortRepoMock = new Mock<IChargingPortRepo>();

            _chargingStationService = new ChargingStationService(
                _chargingStationRepoMock.Object,
                _chargingStationAssemblerMock.Object,
                _chargingPortRepoMock.Object
            );
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllStations()
        {
            // Arrange
            var stations = new List<ChargingStation>
            {
                new ChargingStation { Id = Guid.NewGuid(), Name = "Station A" },
                new ChargingStation { Id = Guid.NewGuid(), Name = "Station B" }
            };

            _chargingStationRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(stations.AsQueryable().BuildMock());

            _chargingStationAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ChargingStation>>()))
                .Returns<List<ChargingStation>>(input => input.Select(cs => new ChargingStationDto { Id = cs.Id, Name = cs.Name }).ToList());

            // Act
            var result = await _chargingStationService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetNumberOfStationsAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            var stations = new List<ChargingStation>
            {
                new ChargingStation { Id = Guid.NewGuid(), Name = "Station A" },
                new ChargingStation { Id = Guid.NewGuid(), Name = "Station B" },
                new ChargingStation { Id = Guid.NewGuid(), Name = "Station C" }
            };

            _chargingStationRepoMock
                .Setup(repo => repo.GetNumberOfChargingStationsAsync())
                .ReturnsAsync(stations.Count);

            // Act
            var result = await _chargingStationService.GetNumberOfChargingStationsAsync();

            // Assert
            Assert.AreEqual(3, result);
        }


        [TestMethod]
        public async Task CanDelete_ShouldReturnTrue_WhenNoPorts()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var stations = new List<ChargingStation>
            {
                new ChargingStation { Id = stationId, Ports = new List<ChargingPort>() }
            };

            _chargingStationRepoMock
                .Setup(repo => repo.GetAllWithNavigation())
                .Returns(stations.AsQueryable().BuildMock());

            // Act
            var result = await _chargingStationService.CanDelete(stationId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CanDelete_ShouldReturnTrue_WhenAllPortsAreDisabled()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var stations = new List<ChargingStation>
            {
                new ChargingStation
                {
                    Id = stationId,
                    Ports = new List<ChargingPort>
                    {
                        new ChargingPort { Id = Guid.NewGuid(), IsDisabled = true },
                        new ChargingPort { Id = Guid.NewGuid(), IsDisabled = true }
                    }
                }
            };

            _chargingStationRepoMock
                .Setup(repo => repo.GetAllWithNavigation())
                .Returns(stations.AsQueryable().BuildMock());

            // Act
            var result = await _chargingStationService.CanDelete(stationId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CanDelete_ShouldReturnFalse_WhenActivePortsExist()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var stations = new List<ChargingStation>
            {
                new ChargingStation
                {
                    Id = stationId,
                    Ports = new List<ChargingPort>
                    {
                        new ChargingPort { Id = Guid.NewGuid(), IsDisabled = false },
                        new ChargingPort { Id = Guid.NewGuid(), IsDisabled = true }
                    }
                }
            };

            _chargingStationRepoMock
                .Setup(repo => repo.GetAllWithNavigation())
                .Returns(stations.AsQueryable().BuildMock());

            // Act
            var result = await _chargingStationService.CanDelete(stationId);

            // Assert
            Assert.IsFalse(result);
        }


    }
}
