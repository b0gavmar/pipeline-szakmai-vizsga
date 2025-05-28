using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.ChargingStationServices.ChargingPortServices
{
    public interface IChargingPortService : IBaseService<ChargingPort, ChargingPortDto>
    {
        Task<int> GetNumberOfPortsAsync();
        Task<int> GetNumberOfInUsePortsAsync();
        Task<int> GetNumberOfEnabledPortsAsync();
        Task<IEnumerable<ChargingPortDto>> GetFilteredPortsByChargingStationIdAsync(int? input, Guid id);
        Task<Response> SetPortToIsBeingUsedByIdAsync(Guid? id);
        Task<Response> SetPortToIsBeingUsedFalseByIdAsync(Guid? id);
        Task<Response> SetPortToIsChargingByIdAsync(Guid? id);
        Task<Response> SetPortToIsChargingFalseByIdAsync(Guid? id);
        Task<int> GetNumberOfPortsOfChargingStationAsync(Guid id);
    }
}
