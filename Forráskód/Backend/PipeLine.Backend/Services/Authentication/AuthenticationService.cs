using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using PipeLine.Backend.Services.Authentication;
using PipeLine.HttpService.Dtos;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace PipeLine.Backend.Services.Authentication
{
    

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginRegisterRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.ToUpper());
            if (user == null)
            {
                return new AuthenticationResponse{ Success = false, ErrorMessage = "The email and password do not match" };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "The email and password do not match" };
            }

            var hasRole = await _userManager.IsInRoleAsync(user, "User");
            if (!hasRole)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "You can't log into this site with that account" };
            }

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, request.StayLoggedIn);

            return new AuthenticationResponse
            {
                Success = true,
                Token = token,
                Message = "Login successful"
            };
        }

        public async Task<AuthenticationResponse> LoginAsAdminAsync(LoginRegisterRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.ToUpper());
            if (user == null)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "The email and password do not match" };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "The email and password do not match" };
            }

            var hasRole = await _userManager.IsInRoleAsync(user, "Admin");
            if (!hasRole)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "You can't log into this site with that account" };
            }

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, request.StayLoggedIn);

            return new AuthenticationResponse
            {
                Success = true,
                Token = token,
                Message = "Login successful"
            };
        }

        public async Task<AuthenticationResponse> RegisterAsync(LoginRegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Email and password are required." };
            }

            if (!request.Email.Contains('@') && !request.Email.Contains('.'))
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Email is not in the correct format." };
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email.ToUpper());
            if (existingUser != null)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Email is already in use." };
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "User registration failed." };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, request.StayLoggedIn);

            return new AuthenticationResponse
            {
                Success = true,
                Token = token,
                Message = "Registration successful"
            };
        }

        public async Task<AuthenticationResponse> RegisterAsAdminAsync(LoginRegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Email and password are required." };
            }

            if (!request.Email.Contains('@') && !request.Email.Contains('.'))
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Email is not in the correct format." };
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email.ToUpper());
            if (existingUser != null)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Email is already in use." };
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "User registration failed." };
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            return new AuthenticationResponse
            {
                Success = true,
                Message = "Registration successful"
            };
        }

        public async Task<AuthenticationResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CurrentPassword) || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Current password and new password are required." };
            }

            var user = await _userManager.FindByEmailAsync(request.Email.ToUpper());
            if (user == null)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "User not found." };
            }

            if (string.IsNullOrEmpty(user.UserName))
            {
                user.UserName = user.Email;
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                return new AuthenticationResponse { Success = false, ErrorMessage = "Password change failed."};
            }

            return new AuthenticationResponse { Success = true, Message = "Password changed successfully" };
        }
    }
}
