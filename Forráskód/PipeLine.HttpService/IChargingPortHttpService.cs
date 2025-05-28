using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.DeviceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public interface IChargingPortHttpService : IBaseHttpService<ChargingPort>
    {
        Task<int> GetNumberOfPortsAsync();
        Task<int> GetNumberOfInUsePortsAsync();
        Task<int> GetNumberOfEnabledPortsAsync();
        Task<List<ChargingPortDto>> GetChargingPortsOfStationsAsync(Guid input);
    }
}
