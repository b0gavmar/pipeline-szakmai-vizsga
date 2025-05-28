using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Backend.Services.ChargingInstanceServices;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace PipeLine.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChargingInstanceController : BaseController<ChargingInstance, ChargingInstanceDto>
    {
        private readonly IChargingInstanceService _chargingInstanceService;

        public ChargingInstanceController(IChargingInstanceService chargingInstanceService)
            : base(chargingInstanceService)
        {
            _chargingInstanceService = chargingInstanceService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("myFilteredInstances")]
        public async Task<IActionResult> GetMyFilteredChargingInstances([FromQuery] DateTime? start = null, [FromQuery] DateTime? end = null)
        {
            try{
                var userId = GetUserId();
                if (userId == null) return Unauthorized();


                var result = await _chargingInstanceService.GetFilteredInstancesByUserIdAsync((Guid)userId, start, end);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when accessing user's chargingInstances: {e.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("myFilteredFinishedInstances")]
        public async Task<IActionResult> GetMyFilteredFinishedChargingInstances([FromQuery] DateTime? start = null, [FromQuery] DateTime? end = null)
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();


                var result = await _chargingInstanceService.GetFilteredInstancesByUserIdAsync((Guid)userId, start, end, true);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when accessing user's finished chargingInstances: {e.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("myOngoingInstances")]
        public async Task<IActionResult> GetOngoingChargingInstances()
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();


                var result = await _chargingInstanceService.GetFilteredInstancesByUserIdAsync((Guid)userId, null, null, false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when accessing user's ongoing chargingInstances: {e.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("myNumberOfInstances")]
        public async Task<IActionResult> GetMyNumberOfInstances()
        {
            try{
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var result = await _chargingInstanceService.GetNumberOfInstancesByUserIdAsync((Guid)userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when accessing user's number of chargingInstances: {e.Message}"));
            }
        }


        [HttpGet("NumberOfInstances")]
        public async Task<IActionResult> GetNumberOfInstances()
        {
            try
            {
                var result = await _chargingInstanceService.GetNumberOfInstancesAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when accessing number of chargingInstances: {e.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost()]
        public override async Task<IActionResult> CreateAsync([FromBody] ChargingInstanceDto dto)
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var result = await _chargingInstanceService.StartChargingInstanceAsync(dto);
                return result.HasError ? BadRequest(result) : Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when creating chargingInstance: {e.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("finishInstance")]
        public async Task<IActionResult> EndInstanceAsync([FromBody] ChargingInstanceDto dto)
        {
            try
            {
                var userId = GetUserId();
                if (userId == null) return Unauthorized();

                var result = await _chargingInstanceService.FinishChargingInstanceAsync(dto);
                return result.HasError ? BadRequest(result) : Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when finishing chargingInstance: {e.Message}"));
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("ChargingProgress/{instanceId}")]
        public async Task<IActionResult> UpdateChargingProgress(Guid instanceId)
        {
            try
            {
                var result = await _chargingInstanceService.UpdateChargingProgressAsync(instanceId);
                return result.HasError ? BadRequest(result) : Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response($"Error when updating chargingInstance progress: {e.Message}"));
            }
        }

        private Guid? GetUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(id, out var guid) ? guid : null;
        }
    }
}
