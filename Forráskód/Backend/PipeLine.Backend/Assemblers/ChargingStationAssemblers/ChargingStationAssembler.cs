using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Converters.ChargingStationConverters;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;

namespace PipeLine.Backend.Assemblers.ChargingStationAssemblers
{
    public class ChargingStationAssembler : Assembler<ChargingStation, ChargingStationDto>, IChargingStationAssembler
    {
        public override ChargingStationDto ToDto(ChargingStation model)
        {
            return model.ToDto();
        }

        public override ChargingStation ToModel(ChargingStationDto dto)
        {
            return dto.ToModel();
        }
    }
}
