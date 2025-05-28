using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Shared.Converters;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.UserModels;

namespace PipeLine.Backend.Assemblers.UserAssemblers
{
    public class ApplicationUserAssembler : Assembler<ApplicationUser, ApplicationUserDto>, IApplicationUserAssembler
    {
        public override ApplicationUserDto ToDto(ApplicationUser applicationUser)
        {
            return applicationUser.ToDto();
        }
        public override ApplicationUser ToModel(ApplicationUserDto dto)
        {
            return dto.ToModel();
        }
    }
}
