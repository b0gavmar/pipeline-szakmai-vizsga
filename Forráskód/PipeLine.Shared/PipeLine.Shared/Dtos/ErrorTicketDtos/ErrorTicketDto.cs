using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Dtos.ErrorTicketDtos
{
    public class ErrorTicketDto
    {
        public Guid Id { get; set; }

        public Guid ChargingStationId { get; set; }

        public string Description { get; set; } = string.Empty;
        public bool IsSolved { get; set; }
    }
}
