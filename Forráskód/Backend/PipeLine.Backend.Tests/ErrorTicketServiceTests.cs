using MockQueryable;
using Moq;
using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Backend.Repos.ErrorTicketRepos;
using PipeLine.Backend.Services.ChargingInstanceServices;
using PipeLine.Backend.Services.ErrorTicketServices;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Backend.Tests
{
    [TestClass]
    public class ErrorTicketServiceTests
    {
        private readonly Mock<IErrorTicketRepo> _errorTicketRepoMock;
        private readonly Mock<IErrorTicketAssembler> _errorTicketAssemblerMock;

        private readonly IErrorTicketService _errorTicketService;

        public ErrorTicketServiceTests()
        {
            _errorTicketRepoMock = new Mock<IErrorTicketRepo>();
            _errorTicketAssemblerMock = new Mock<IErrorTicketAssembler>();

            _errorTicketService = new ErrorTicketService(
                _errorTicketRepoMock.Object,
                _errorTicketAssemblerMock.Object
            );
        }

        [TestMethod]
        public async Task GetAllErrorTickets_ShouldReturnAllTickets()
        {
            // Arrange
            var errorTickets = new List<ErrorTicket>
            {
                new ErrorTicket { Id = Guid.NewGuid() },
                new ErrorTicket { Id = Guid.NewGuid() }
            };

            _errorTicketRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(errorTickets.AsQueryable().BuildMock());

            _errorTicketAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ErrorTicket>>()))
                .Returns<List<ErrorTicket>>(input => input.Select(e => new ErrorTicketDto { Id = e.Id }).ToList());

            // Act
            var result = await _errorTicketService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetFilteredErrorTicketsByStationIdAsync_ShouldReturnFilteredTickets()
        {
            // Arrange
            var stationId = Guid.NewGuid();
            var errorTickets = new List<ErrorTicket>
            {
                    new ErrorTicket
                    {
                        Id = Guid.NewGuid(),
                        ChargingStationId = stationId,
                        ChargingStation = new PipeLine.Shared.Models.ChargingStationModels.ChargingStation
                        {
                            Id = stationId,
                            Name = "Station A"
                        }
                    },
                    new ErrorTicket
                    {
                        Id = Guid.NewGuid(),
                        ChargingStationId = Guid.NewGuid(),
                        ChargingStation = new PipeLine.Shared.Models.ChargingStationModels.ChargingStation
                        {
                            Id = Guid.NewGuid(),
                            Name = "Station B"
                        }
                    }
            };

            var filteredTickets = errorTickets.Where(e => e.ChargingStationId == stationId).AsQueryable().BuildMock();

            _errorTicketRepoMock
                .Setup(repo => repo.GetAllTicketsByChargingStationIdAsync(stationId))
                .Returns(filteredTickets);

            _errorTicketAssemblerMock
                .Setup(assembler => assembler.ToDtos(It.IsAny<List<ErrorTicket>>()))
                .Returns<List<ErrorTicket>>(input => input.Select(e => new ErrorTicketDto { Id = e.Id }).ToList());

            // Act
            var result = await _errorTicketService.GetAllTicketsByChargingStationIdAsync(stationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

    }
}