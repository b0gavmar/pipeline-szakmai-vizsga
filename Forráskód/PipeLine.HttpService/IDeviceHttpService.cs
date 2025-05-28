using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public interface IDeviceHttpService : IBaseHttpService<Device>
    {
        Task<int> GetNumberOfEBikesAsync();
        Task<int> GetNumberOfEScootersAsync();
        Task<int> GetNumberOfESkateBoardsAsync();
        Task<int> GetNumberOfDevicesWithANameAsync();
        Task<int> GetNumberOfEBikesWithDetachableBatteryAsync();
        Task<int> GetNumberOfFoldableEScootersAsync();
        Task<int> GetNumberOfLocakbleESkateBoardsAsync();
        Task<List<DeviceDto>> GetFilteredDevicesAsync(string input);
    }
}
