using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Converters
{
    public static class ApplicationUserConverter
    {
        public static ApplicationUserDto ToDto(this ApplicationUser applicationUser)
        {
            return new ApplicationUserDto
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                FirstName = applicationUser.FirstName ?? string.Empty,
                LastName = applicationUser.LastName ?? string.Empty,
                PhoneNumber = applicationUser.PhoneNumber ?? string.Empty
            };
        }

        public static ApplicationUser ToModel(this ApplicationUserDto dto)
        {
            return new ApplicationUser
            {
                Id = dto.Id,
                UserName = dto.UserName,
                NormalizedUserName = dto.UserName.ToUpper(),
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpper(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber
            };
        }
    }
}
