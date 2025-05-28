using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Repos.ChargingStationRepos
{
    public interface IChargingStationRepo : IBaseRepo<ChargingStation>
    {
        IQueryable<ChargingStation> GetAllWithNavigation();
        Task<int> GetNumberOfChargingStationsAsync();
        IQueryable<ChargingStation> GetFilteredStationsAsync(string input);
    }
}
