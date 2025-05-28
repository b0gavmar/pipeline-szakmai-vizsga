using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;

namespace PipeLine.Backend.Services.ChargingStationServices
{
    public interface IChargingStationService : IBaseService<ChargingStation, ChargingStationDto>
    {
        Task<int> GetNumberOfChargingStationsAsync();
        Task<IEnumerable<ChargingStationDto>> GetFilteredStationsAsync(string input);
    }
}
