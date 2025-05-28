using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Converters;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Models.ChargingInstanceModels;

namespace PipeLine.Backend.Assemblers.ChargingInstanceAssemblers
{
    public class ChargingInstanceAssembler : Assembler<ChargingInstance, ChargingInstanceDto>,IChargingInstanceAssembler
    {
        public override ChargingInstanceDto ToDto(ChargingInstance model)
        {
            return model.ToDto();
        }


        public override ChargingInstance ToModel(ChargingInstanceDto dto)
        {
            return dto.ToModel();
        }

    }
}
