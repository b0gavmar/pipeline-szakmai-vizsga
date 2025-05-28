using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public interface IApplicationUserHttpService : IBaseHttpService<ApplicationUser>
    {
        Task<int> GetNumberOfUsersAsync();
        Task<int> GetNumberOfUsersWithEmailAsync();
        Task<int> GetNumberOfUsersWithLastNameAsync();
        Task<int> GetNumberOfUsersWithFirstNameAsync();
        Task<int> GetNumberOfUsersWithPhoneNumber();
        Task<ApplicationUserDto> GetUserByEmailAsync(string email);
    }
}
