using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public class ChargingPortHttpService : BaseHttpService<ChargingPort, ChargingPortDto>, IChargingPortHttpService
    {
        public ChargingPortHttpService(IHttpClientFactory? httpClientFactory, IChargingPortAssembler assembler) : base(httpClientFactory, assembler)
        {
        }

        public ChargingPortHttpService() : base()
        {
            
        }
        public async Task<int> GetNumberOfPortsAsync()
        {
            try
            {
                int numberOfPorts = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfPorts");
                return numberOfPorts;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetNumberOfInUsePortsAsync()
        {
            try
            {
                int numberOfInUsePorts = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfInUsePorts");
                return numberOfInUsePorts;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> GetNumberOfEnabledPortsAsync()
        {
            try
            {
                int numberOfEnabledPorts = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfEnabledPorts");
                return numberOfEnabledPorts;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<ChargingPortDto>> GetChargingPortsOfStationsAsync(Guid input)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChargingPortDto>>($"{path}/FilteredPortsOfChargingStation/{input}");
                return response ?? new List<ChargingPortDto>();
            }
            catch (Exception ex)
            {
                return new List<ChargingPortDto>();
            }
        }

    }
}
