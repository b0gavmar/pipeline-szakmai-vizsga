using System;
using System.Collections.Generic;
using PipeLine.Shared.Models.ChargingStationModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipeLine.Shared.Dtos.ChargingStationDto;

namespace PipeLine.HttpService
{
    public interface IChargingStationHttpService : IBaseHttpService<ChargingStation>
    {
        Task<int> GetChargingStationCountAsync();
        Task<List<ChargingStationDto>> GetFilteredChargingStationsAsync(string? input);
    }
}
