using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto
{
    public class ChargingPortDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsBeingUsed { get; set; } = false;
        public bool IsDisabled { get; set; } = false;
        public Guid ChargingStationId { get; set; }
        public int PortNumber { get; set; }
        public double MaxChargingSpeed { get; set; }
        public bool IsCharging { get; set; } = false;
    }
}
