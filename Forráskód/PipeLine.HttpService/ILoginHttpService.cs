using PipeLine.Shared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public interface ILoginHttpService
    {
        Task<string?> LoginAsync(LoginRegisterRequest loginDto);
        Task<string?> RegisterAsync(LoginRegisterRequest registerDto);
        Task<bool> LogoutAsync();
    }
}
