using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Converters.ChargingStationConverters.ChargingPortConverter;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;

namespace PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers
{
    public class ChargingPortAssembler : Assembler<ChargingPort, ChargingPortDto>, IChargingPortAssembler
    {
        public override ChargingPortDto ToDto(ChargingPort model)
        {
            return model.ToDto();
        }
        public override ChargingPort ToModel(ChargingPortDto dto)
        {
            return dto.ToModel();
        }
    }
}
