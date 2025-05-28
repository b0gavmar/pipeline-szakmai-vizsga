using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;

namespace PipeLine.Backend.Assemblers.ChargingStationAssemblers
{
    public interface IChargingStationAssembler : IAssembler<ChargingStation, ChargingStationDto>
    {

    }
}
