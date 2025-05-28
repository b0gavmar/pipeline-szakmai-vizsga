using Microsoft.AspNetCore.Mvc;
using PipeLine.Backend.Repos.ChargingStationRepos;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Responses;
using PipeLine.Shared.Converters;
using PipeLine.Shared.Converters.ChargingStationConverters;
using PipeLine.Shared.Dtos.ChargingStationDto;
using System.Xml.Linq;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using Microsoft.EntityFrameworkCore;
using PipeLine.Shared.Dtos;
using PipeLine.Backend.Services.ChargingStationServices;

namespace PipeLine.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChargingStationController : BaseController<ChargingStation, ChargingStationDto>
    {
        private readonly IChargingStationService _chargingStationService;

        public ChargingStationController(IChargingStationService service)
            : base(service)
        {
            _chargingStationService = service;
        }

        [HttpGet("NumberOfStations")]
        public async Task<IActionResult> GetChargingStationCountAsync()
        {
            try
            {
                return Ok(await _chargingStationService.GetNumberOfChargingStationsAsync());
            }
            catch (Exception e)
            {
                return StatusCode(500,new Response($"Error while getting number of stations: {e.Message}"));
            }
        }

        [HttpGet("FilteredStations")]
        public async Task<IActionResult> GetFilteredChargingStationsAsync([FromQuery] string? input = "")
        {
            try
            {
                return Ok(await _chargingStationService.GetFilteredStationsAsync(input));
            }
            catch (Exception e)
            {
                return StatusCode(500,new Response($"Error while getting filtered stations: {e.Message}"));
            }
        }

        [HttpGet("FilteredPagedStations")]
        public async Task<IActionResult> GetFilteredPagedChargingStations([FromQuery] PaginationParameters paging, [FromQuery] string input = "")
        {
            try
            {
                var allStations = await _chargingStationService.GetFilteredStationsAsync(input);
                var paged = allStations
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize);

                return Ok(paged);
            }
            catch (Exception e)
            {
                return StatusCode(500,new Response($"Error while getting filtered stations: {e.Message}"));
            }
        }
    }
    
}
