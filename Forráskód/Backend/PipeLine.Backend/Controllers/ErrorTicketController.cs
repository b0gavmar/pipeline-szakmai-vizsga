using Microsoft.AspNetCore.Mvc;
using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using PipeLine.Backend.Services.ErrorTicketServices;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErrorTicketController : BaseController<ErrorTicket, ErrorTicketDto>
    {
        private readonly IErrorTicketService _errorTicketService;

        public ErrorTicketController(IErrorTicketService service)
            : base(service)
        {
            _errorTicketService = service;
        }

        [HttpGet("NumberOfUnsolved")]
        public async Task<IActionResult> GetNumberOfUnsolvedAsync()
        {
            try
            {
                return Ok(await _errorTicketService.GetNumberOfUnsolvedAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of unsolved tickets: {ex.Message}"));
            }
        }

        [HttpGet("TicketsOfChargingStation/{id}")]
        public async Task<IActionResult> GetTicketsOfChargingStationAsync(Guid id)
        {
            try
            {
                return Ok(await _errorTicketService.GetAllTicketsByChargingStationIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting tickets of charging station: {ex.Message}"));
            }
        }
    }
}
