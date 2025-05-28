using Microsoft.AspNetCore.Mvc;
using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using System.Collections.Generic;

namespace PipeLine.Backend.Services.DeviceServices
{
    public interface IDeviceService : IBaseService<Device,DeviceDto>
    {
        Task<IEnumerable<DeviceDto>> GetNonChargingDevicesByUserId(Guid id);
        Task<int> GetNumberOfDevicesByUserIdAsync(Guid Id);
        Task<int> GetNumberOfEbikesAsync();
        Task<int> GetNumberOfEScootersAsync();
        Task<int> GetNumberOfESkateBoardsAsync();
        Task<int> GetNumberOfDevicesWithANameAsync();
        Task<int> GetNumberOfEBikesWithDetachableBatteryAsync();
        Task<int> GetNumberOfFoldableEScootersAsync();
        Task<int> GetNumberOfLocakbleESkateBoardsAsync();
        Task<IEnumerable<DeviceDto>> GetDevicesByUserIdAsync(Guid Id);
        Task<IEnumerable<DeviceDto>> GetFilteredDevicesByUserIdAsync(string? input, Guid Id);
        Task<IEnumerable<DeviceDto>> GetFilteredDevicesAsync(string? input);
    }
}
