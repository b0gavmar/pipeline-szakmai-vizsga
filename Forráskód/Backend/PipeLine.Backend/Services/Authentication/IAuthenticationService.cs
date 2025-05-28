using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> LoginAsync(LoginRegisterRequest request);
        Task<AuthenticationResponse> LoginAsAdminAsync(LoginRegisterRequest request);
        Task<AuthenticationResponse> RegisterAsync(LoginRegisterRequest request);
        Task<AuthenticationResponse> RegisterAsAdminAsync(LoginRegisterRequest request);
        Task<AuthenticationResponse> ChangePasswordAsync(ChangePasswordRequest request);
    }
}
