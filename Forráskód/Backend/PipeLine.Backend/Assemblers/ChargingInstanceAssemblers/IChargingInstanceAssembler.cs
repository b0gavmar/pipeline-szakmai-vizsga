using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Models.ChargingInstanceModels;

namespace PipeLine.Backend.Assemblers.ChargingInstanceAssemblers
{
    public interface IChargingInstanceAssembler : IAssembler<ChargingInstance, ChargingInstanceDto>
    {
    }
}
