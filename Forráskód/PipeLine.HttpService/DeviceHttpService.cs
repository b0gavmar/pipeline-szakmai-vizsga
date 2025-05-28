using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using System.Net.Http.Json;

namespace PipeLine.HttpService
{
    public class DeviceHttpService: BaseHttpService<Device,DeviceDto>, IDeviceHttpService
    {
        public DeviceHttpService(IHttpClientFactory? httpClientFactory, IDeviceAssembler assembler) : base(httpClientFactory, assembler)
        {
        }

        public DeviceHttpService(): base() 
        {
            
        }

        public async Task<int> GetNumberOfEBikesAsync()
        {
            try
            {
                int numberOfEBikes = await _httpClient.GetFromJsonAsync<int>(path+"/NumberOfEBikes");
                return numberOfEBikes;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async  Task<int> GetNumberOfEScootersAsync()
        {
            try
            {
                int numberOfEScooters = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfEScooters");
                return numberOfEScooters;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfESkateBoardsAsync()
        {
            try
            {
                int numberOfESkateBoards = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfESkateBoards");
                return numberOfESkateBoards;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        public async Task<int> GetNumberOfDevicesWithANameAsync()
        {
            try
            {
                int numberOfDevices = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfDevicesWithAName");
                return numberOfDevices;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        public async Task<int> GetNumberOfEBikesWithDetachableBatteryAsync()
        {
            try
            {
                int numberOfEBikes = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfEBikesWithDetachableBattery");
                return numberOfEBikes;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfFoldableEScootersAsync()
        {
            try
            {
                int numberOfEScooters = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfFoldableEScooters");
                return numberOfEScooters;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfLocakbleESkateBoardsAsync()
        {
            try
            {
                int numberOfESkateBoards = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfLockableESkateBoards");
                return numberOfESkateBoards;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        public async Task<List<DeviceDto>> GetFilteredDevicesAsync(string input)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<DeviceDto>>($"{path}/FilteredDevices?input={Uri.EscapeDataString(input)}");
                return response ?? new List<DeviceDto>();
            }
            catch (Exception ex)
            {
                return new List<DeviceDto>();
            }
        }
    }
}
