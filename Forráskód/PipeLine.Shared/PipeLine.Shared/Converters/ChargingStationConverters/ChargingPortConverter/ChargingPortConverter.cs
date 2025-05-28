using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Converters.ChargingStationConverters.ChargingPortConverter
{
    public static class ChargingPortConverter
    {
        public static ChargingPortDto ToDto(this ChargingPort chargingPort)
        {
            return new ChargingPortDto
            {
                Id = chargingPort.Id,
                IsBeingUsed = chargingPort.IsBeingUsed,
                IsDisabled = chargingPort.IsDisabled,
                ChargingStationId = chargingPort.ChargingStationId,
                PortNumber = chargingPort.PortNumber,
                MaxChargingSpeed = chargingPort.MaxChargingSpeed,
                IsCharging = chargingPort.IsCharging,
            };
        }

        public static ChargingPort ToModel(this ChargingPortDto chargingPortDto)
        {
            return new ChargingPort
            {
                Id = chargingPortDto.Id,
                IsBeingUsed = chargingPortDto.IsBeingUsed,
                IsDisabled = chargingPortDto.IsDisabled,
                ChargingStationId = chargingPortDto.ChargingStationId,
                PortNumber = chargingPortDto.PortNumber,
                MaxChargingSpeed = chargingPortDto.MaxChargingSpeed,
                IsCharging = chargingPortDto.IsCharging,
            };
        }
    }
}
