using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Converters.ChargingStationConverters
{
    public static class ChargingStationConverter
    {
        public static ChargingStationDto ToDto(this ChargingStation model)
        {
            return new ChargingStationDto
            {
                Id = model.Id,
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address = model.Address
            };
        }

        public static ChargingStation ToModel(this ChargingStationDto dto)
        {
            return new ChargingStation
            {
                Id = dto.Id,
                Name = dto.Name,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Address = dto.Address
            };
        }
    }
}
