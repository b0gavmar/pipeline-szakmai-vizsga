using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.ChargingInstanceServices
{
    public interface IChargingInstanceService : IBaseService<ChargingInstance, ChargingInstanceDto>
    {
        Task<ChargingProgressResponse> UpdateChargingProgressAsync(Guid instanceId);
        Task<Response> FinishChargingInstanceAsync(ChargingInstanceDto dto);
        Task<Response> StartChargingInstanceAsync(ChargingInstanceDto dto);
        Task<IEnumerable<ChargingInstanceDto>> GetFilteredInstancesByUserIdAsync(Guid userId, DateTime? start, DateTime? end, bool? state = null);
        Task<int> GetNumberOfInstancesByUserIdAsync(Guid userId);
        Task<int> GetNumberOfInstancesAsync();
        Task<ChargingInstance> GetInstanceByIdAsync(Guid id);
    }
}
