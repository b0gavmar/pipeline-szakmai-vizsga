using Microsoft.AspNetCore.Mvc;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Converters.ChargingStationConverters.ChargingPortConverter;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Backend.Repos.ChargingStationRepos;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.Backend.Services.ChargingStationServices.ChargingPortServices;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChargingPortController : BaseController<ChargingPort, ChargingPortDto>
    {
        private readonly IChargingPortService _chargingPortService;

        public ChargingPortController(IChargingPortService chargingPortService)
            : base(chargingPortService)
        {
            _chargingPortService = chargingPortService;
        }

        [HttpGet("NumberOfPorts")]
        public async Task<IActionResult> GetNumberOfPortsAsync()
        {
            try
            {
                return Ok(await _chargingPortService.GetNumberOfPortsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting the number of ports: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfInUsePorts")]
        public async Task<IActionResult> GetNumberOfInUsePortsAsync()
        {
            try
            {
                return Ok(await _chargingPortService.GetNumberOfInUsePortsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of ports in use: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfEnabledPorts")]
        public async Task<IActionResult> GetNumberOfEnabledPortsAsync()
        {
            try
            {
                return Ok(await _chargingPortService.GetNumberOfEnabledPortsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of enabled ports: {ex.Message}"));
            }
        }

        [HttpGet("FilteredPortsOfChargingStation/{id}")]
        public async Task<IActionResult> GetPortsOfChargingStationAsync(Guid id, [FromQuery] int? input = null)
        {
            try
            {
                return Ok(await _chargingPortService.GetFilteredPortsByChargingStationIdAsync(input, id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting ports: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfPortsOfChargingStation/{id}")]
        public async Task<IActionResult> GetNumberOfPortsOfChargingStationAsync(Guid id)
        {
            try
            {
                return Ok(await _chargingPortService.GetNumberOfPortsOfChargingStationAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting ports of station: {ex.Message}"));
            }
        }
    }
}
