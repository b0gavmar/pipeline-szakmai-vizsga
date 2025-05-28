using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Converters
{
    public static class ErrorTicketConverter
    {
        public static ErrorTicketDto ToDto(this ErrorTicket errorTicket)
        {
            return new ErrorTicketDto
            {
                Id = errorTicket.Id,
                ChargingStationId = errorTicket.ChargingStationId,
                Description = errorTicket.Description,
                IsSolved = errorTicket.IsSolved
            };
        }

        public static ErrorTicket ToModel(this ErrorTicketDto dto)
        {
            return new ErrorTicket
            {
                Id = dto.Id,
                ChargingStationId = dto.ChargingStationId,
                Description = dto.Description,
                IsSolved = dto.IsSolved
            };
        }
    }
}
