using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Backend.Assemblers.UserAssemblers;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public class ApplicationUserHttpService : BaseHttpService<ApplicationUser, ApplicationUserDto>,IApplicationUserHttpService
    {
        public ApplicationUserHttpService(IHttpClientFactory? httpClientFactory, IApplicationUserAssembler assembler) :  base(httpClientFactory, assembler)
        {
        }

        public ApplicationUserHttpService() : base()
        {
        }


        public async Task<ApplicationUserDto> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<ApplicationUserDto>(path + "/" + email);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> GetNumberOfUsersAsync()
        {
            try
            {
                int numberOfUsers = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfUsers");
                return numberOfUsers;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfUsersWithEmailAsync()
        {
            try
            {
                int numberOfUsers = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfUsersWithEmail");
                return numberOfUsers;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfUsersWithFirstNameAsync()
        {
            try
            {
                int numberOfUsers = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfUsersWithFirstName");
                return numberOfUsers;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfUsersWithLastNameAsync()
        {
            try
            {
                int numberOfUsers = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfUsersWithLastName");
                return numberOfUsers;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfUsersWithPhoneNumber()
        {
            try
            {
                int numberOfUsers = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfUsersWithPhoneNumber");
                return numberOfUsers;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
