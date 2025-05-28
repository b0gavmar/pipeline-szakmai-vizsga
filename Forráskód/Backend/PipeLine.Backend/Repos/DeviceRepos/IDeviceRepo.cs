using Microsoft.AspNetCore.Mvc;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;


namespace PipeLine.Backend.Repos.DeviceRepos
{
    public interface IDeviceRepo : IBaseRepo<Device>
    {
        IQueryable<Device> GetAllWithNavigation();
        Task<int> GetNumberOfDevicesByUserIdAsync(Guid Id);
        Task<int> GetNumberOfEbikesAsync();
        Task<int> GetNumberOfEScootersAsync();
        Task<int> GetNumberOfESkateBoardsAsync();
        Task<int> GetNumberOfDevicesWithANameAsync();
        Task<int> GetNumberOfEBikesWithDetachableBatteryAsync();
        Task<int> GetNumberOfFoldableEScootersAsync();
        Task<int> GetNumberOfLocakbleESkateBoardsAsync();
        IQueryable<Device> GetEBikesWithANameAndDetachableBattery();
        IQueryable<Device> GetDevicesByUserId(Guid Id);
        IQueryable<Device> GetFilteredDevicesByUserId(string? input, Guid Id);
        IQueryable<Device> GetFilteredDevices(string? input);
        IQueryable<Device> GetNonChargingDevicesByUserId(Guid id);
    }
}
