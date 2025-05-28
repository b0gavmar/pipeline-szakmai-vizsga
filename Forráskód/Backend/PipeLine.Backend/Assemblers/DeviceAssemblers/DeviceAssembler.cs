using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Converters;

namespace PipeLine.Backend.Assemblers.DeviceAssemblers
{
    public class DeviceAssembler : Assembler<Device, DeviceDto>, IDeviceAssembler
    {
        public override DeviceDto ToDto(Device device)
        {
            return device.ToDto();
        }
        public override Device ToModel(DeviceDto dto)
        {
            return dto.ToModel();
        }
    }
}

