using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Repos.ChargingInstanceRepos
{
    public interface IChargingInstanceRepo : IBaseRepo<ChargingInstance>
    {
        IQueryable<ChargingInstance> GetAllWithNavigation();
        Task<int> GetNumberOfInstancesAsync();
        Task<int> GetNumberOfInstancesByUserIdAsync(Guid id);
        IQueryable<ChargingInstance> GetInstancesByDeviceIdAsync(Guid id);
        IQueryable<ChargingInstance> GetInstancesByPortId(Guid id);
        IQueryable<ChargingInstance> GetFilteredInstancesByUserIdAsync(DateTime? start, DateTime? end, Guid userId, bool? state);
        Task<Response> FinishInstance(ChargingInstance instance);
        Task<ChargingInstance> GetInstanceByIdAsync(Guid id);
    }
}
