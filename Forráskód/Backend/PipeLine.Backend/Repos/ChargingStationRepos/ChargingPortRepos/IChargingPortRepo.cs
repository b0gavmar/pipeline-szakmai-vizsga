using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;

namespace PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos
{
    public interface IChargingPortRepo : IBaseRepo<ChargingPort>
    {
        Task<int> GetNumberOfPortsAsync();
        Task<int> GetNumberOfInUsePortsAsync();
        Task<int> GetNumberOfEnabledPortsAsync();
        Task<IEnumerable<ChargingPort>> GetFilteredPortsByChargingStationIdAsync(int? input,Guid id);
        Task SetPortToIsBeingUsedByIdAsync(Guid? id);
        Task SetPortToIsBeingUsedFalseByIdAsync(Guid? id);
        Task SetPortToIsChargingByIdAsync(Guid? id);
        Task SetPortToIsChargingFalseByIdAsync(Guid? id);
        Task<int> GetNumberOfPortsOfChargingStationAsync(Guid id);
    }
}
