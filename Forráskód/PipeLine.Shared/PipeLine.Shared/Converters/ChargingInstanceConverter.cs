using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingInstanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Converters
{
    public static class ChargingInstanceConverter
    {
        public static ChargingInstanceDto ToDto(this ChargingInstance model)
        {
            return new ChargingInstanceDto
            {
                Id = model.Id,
                ChargingPortId = model.ChargingPortId,
                DeviceId = model.DeviceId,
                Start = model.Start,
                End = model.End,
                StartingPercentage = model.StartingPercentage,
                EndPercentage = model.EndPercentage,
                DesiredEndPercentage = model.DesiredEndPercentage,
                PortNumber = model.ChargingPort?.PortNumber,
                ChargingStationName = model.ChargingPort?.ChargingStation?.Name
            };
        }
        public static ChargingInstance ToModel(this ChargingInstanceDto dto)
        {
            return new ChargingInstance
            {
                Id = dto.Id,
                ChargingPortId = dto.ChargingPortId,
                DeviceId = dto.DeviceId,
                Start = dto.Start,
                End = dto.End,
                StartingPercentage = dto.StartingPercentage,
                EndPercentage = dto.EndPercentage,
                DesiredEndPercentage = dto.DesiredEndPercentage,
            };
        }
    }
}
