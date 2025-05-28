using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public class ChargingStationHttpService: BaseHttpService<ChargingStation, ChargingStationDto>, IChargingStationHttpService
    {
        public ChargingStationHttpService(IHttpClientFactory? httpClientFactory, IChargingStationAssembler assembler) : base(httpClientFactory, assembler)
        {
        }

        public ChargingStationHttpService() : base()
        {
            
        }
        public async Task<int> GetChargingStationCountAsync()
        {
            try
            {
                int numberOfStations = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfStations");
                return numberOfStations;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<ChargingStationDto>> GetFilteredChargingStationsAsync(string? input)
        {
            try
            {
                if(input != null)
                {
                    var response = await _httpClient.GetFromJsonAsync<List<ChargingStationDto>>($"{path}/FilteredStations?input={Uri.EscapeDataString(input)}");
                    return response ?? new List<ChargingStationDto>();
                }
                else
                {
                    var response = await _httpClient.GetFromJsonAsync<List<ChargingStationDto>>($"{path}/FilteredStations");
                    return response;
                }                
            }
            catch (Exception ex)
            {
                return new List<ChargingStationDto>();
            }
        }

    }
}
