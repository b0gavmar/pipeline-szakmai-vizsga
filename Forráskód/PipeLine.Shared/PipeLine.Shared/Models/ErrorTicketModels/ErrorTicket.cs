using PipeLine.Shared.Models.ChargingStationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models.ErrorTicketModels
{
    public class ErrorTicket : IDbEntity<ErrorTicket>
    {
        public Guid Id { get; set; }

        public Guid ChargingStationId { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsSolved { get; set; } 

        public ChargingStation ChargingStation { get; set; }
    }
}
