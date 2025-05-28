using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PipeLine.Backend.Services.Authentication;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Responses;
using IAuthenticationService = PipeLine.Backend.Services.Authentication.IAuthenticationService;

namespace PipeLine.Backend.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRegisterRequest request)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(request);

                if (!result.Success)
                {
                    return Unauthorized(new { message = result.ErrorMessage });
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = request.StayLoggedIn ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(8)
                };

                Response.Cookies.Append("jwt_pipeline", result.Token, cookieOptions);

                return Ok(new { message = result.Message, token = result.Token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while logging in: {ex.Message}"));
            }
        }

        [HttpPost("adminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginRegisterRequest request)
        {
            try
            {
                var result = await _authenticationService.LoginAsAdminAsync(request);

                if (!result.Success)
                {
                    return Unauthorized(new { message = result.ErrorMessage });
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = request.StayLoggedIn ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(8)
                };

                Response.Cookies.Append("jwt_pipeline", result.Token, cookieOptions);

                return Ok(new { message = result.Message, token = result.Token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while logging in: {ex.Message}"));
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("jwt_pipeline", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
                return Ok(new { message = "Token deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while logging out: {ex.Message}"));
            }
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRegisterRequest request)
        {
            try
            {
                var result = await _authenticationService.RegisterAsync(request);

                if (!result.Success)
                {
                    return BadRequest(new { message = result.ErrorMessage });
                }

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = request.StayLoggedIn ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(8)
                };

                Response.Cookies.Append("jwt_pipeline", result.Token, cookieOptions);

                return Ok(new { message = result.Message, token = result.Token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while registering: {ex.Message}"));
            }
        }

        [HttpPost("registerAsAdmin")]
        public async Task<IActionResult> RegisterAsAdmin([FromBody] LoginRegisterRequest request)
        {
            try
            {
                var result = await _authenticationService.RegisterAsAdminAsync(request);
                if (!result.Success)
                {
                    return BadRequest(new { message = result.ErrorMessage });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while registering: {ex.Message}"));
            }
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var result = await _authenticationService.ChangePasswordAsync(request);

                if (!result.Success)
                {
                    return BadRequest(new { message = result.ErrorMessage });
                }

                return Ok(new { message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response($"Error while changing password: {ex.Message}"));
            }
            
        }

    }
}
