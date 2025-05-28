using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Context;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Shared.Responses;
using PipeLine.Backend.Repos.Base;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using PipeLine.Backend.Services.DeviceServices;

namespace PipeLine.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : BaseController<Device, DeviceDto>
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService service)
            : base(service)
        {
            _deviceService = service;
        }

        [HttpGet("NumberOfEBikes")]
        public async Task<IActionResult> GetNumberOfEBikesAsync()
        {
            try
            {
                return Ok(await _deviceService.GetNumberOfEbikesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of Ebikes: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfEScooters")]
        public async Task<IActionResult> GetNumberOfEScootersAsync()
        {
            try
            {
                return Ok(await _deviceService.GetNumberOfEScootersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of EScooters: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfESkateBoards")]
        public async Task<IActionResult> GetNumberOfESkateBoardsAsync()
        {
            try
            {
                return Ok(await _deviceService.GetNumberOfESkateBoardsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of ESkateBoards: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfDevicesWithAName")]
        public async Task<IActionResult> GetNumberOfDevicesWithANameAsync()
        {
            try
            {
                return Ok(await _deviceService.GetNumberOfDevicesWithANameAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of devices with a name: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfEBikesWithDetachableBattery")]
        public async Task<IActionResult> GetNumberOfEBikesWithDetachableBatteryAsync()
        {
            try
            {
                return Ok(await _deviceService.GetNumberOfEBikesWithDetachableBatteryAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of Ebikes with detachable battery: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfFoldableEScooters")]
        public async Task<IActionResult> GetNumberOfFoldableEScootersAsync()
        {
            try
            {
                return Ok(await _deviceService.GetNumberOfFoldableEScootersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of foldable EScooters: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfLocakbleESkateBoards")]
        public async Task<IActionResult> GetNumberOfLocakbleESkateBoardsAsync()
        {
            try
            {
                return Ok(await _deviceService.GetNumberOfLocakbleESkateBoardsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of lockable ESkateBoards: {ex.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("myNonChargingDevices")]
        public async Task<IActionResult> GetDevicesByUserId()
        {
            try
            {
                Guid id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var devices = await _deviceService.GetNonChargingDevicesByUserId(id);
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting devices by user ID: {ex.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("myFilteredDevices")]
        public async Task<IActionResult> GetDevicesByNameModelManufacturerAsync([FromQuery] string input = "")
        {
            try
            {
                Guid id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var devices = await _deviceService.GetFilteredDevicesByUserIdAsync(input, id);
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting filtered devices by user ID: {ex.Message}"));
            }
        }

        [HttpGet("FilteredDevices")]
        public async Task<IActionResult> GetFilteredDevicesAsync([FromQuery] string input = "")
        {
            try
            {
                var devices = await _deviceService.GetFilteredDevicesAsync(input);
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting filtered devices by user ID: {ex.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("myNumberOfDevices")]
        public async Task<IActionResult> GetNumberOfDevicesByUserIdAsync()
        {
            try
            {
                Guid id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(await _deviceService.GetNumberOfDevicesByUserIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of devices by user ID: {ex.Message}"));
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost()]
        public async override Task<IActionResult> CreateAsync([FromBody] DeviceDto deviceDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(userId))
                    return BadRequest("User ID not found in token.");

                if (deviceDto.Id == Guid.Empty)
                    deviceDto.Id = Guid.NewGuid();

                if (deviceDto.ApplicationUserId == Guid.Empty)
                    deviceDto.ApplicationUserId = Guid.Parse(userId);

                var response = await _deviceService.CreateAsync(deviceDto);

                if (!response.HasError)
                    return Ok(response);

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while creating device: {ex.Message}"));
            }
        }
    }
}
