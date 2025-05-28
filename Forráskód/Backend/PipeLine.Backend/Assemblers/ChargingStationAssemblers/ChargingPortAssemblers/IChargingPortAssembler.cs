using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;

namespace PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers
{
    public interface IChargingPortAssembler : IAssembler<ChargingPort, ChargingPortDto>
    {
    }
}
