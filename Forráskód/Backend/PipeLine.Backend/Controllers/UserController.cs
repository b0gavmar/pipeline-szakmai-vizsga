using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Context;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Shared.Responses;
using PipeLine.Backend.Repos.Base;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Backend.Assemblers.UserAssemblers;
using PipeLine.Backend.Repos.UserRepos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using PipeLine.Shared.Dtos.ChargingInstance;
using Org.BouncyCastle.Asn1;
using PipeLine.Backend.Services.UserServices;

namespace PipeLine.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController : BaseController<ApplicationUser, ApplicationUserDto>
    {
        private readonly IUserService _userService;

        public ApplicationUserController(IUserService service)
            : base(service)
        {
            _userService = service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { message = "Invalid token" });

                var userDto = await _userService.GetByIdAsync(Guid.Parse(userId));

                if (userDto is null)
                    return NotFound(new { message = "User not found" });

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting current user: {ex.Message}"));
            }
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmailAsync(string email)
        {
            try
            {
                var entity = await _userService.GetByEmailAsync(email);
                return entity == null ? NotFound(new Response($"No entity found with email = {email}")) : Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfUsers")]
        public async Task<IActionResult> GetNumberOfUsersAsync()
        {
            try
            {
                return Ok(await _userService.GetNumberOfUsersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of users: {ex.Message}"));
            }
        }

        [HttpGet("NumberOfUsersWithEmail")]
        public async Task<IActionResult> GetNumberOfUsersWithEmailAsync()
        {
            try
            {
                return Ok(await _userService.GetNumberOfUsersWithEmailAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while getting number of users with email: {ex.Message}"));
            }
        }
    }
}
