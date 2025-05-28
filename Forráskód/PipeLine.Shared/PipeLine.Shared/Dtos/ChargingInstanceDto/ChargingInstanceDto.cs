using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Dtos.ChargingInstance
{
    public class ChargingInstanceDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? ChargingPortId { get; set; } = Guid.Empty;
        public Guid? DeviceId { get; set; } = null;
        public DateTime? Start {  get; set; }= DateTime.MinValue;
        public DateTime? End { get; set; } = null;
        public int? StartingPercentage { get; set; } = null;
        public int? EndPercentage { get; set; } = null;
        public int? DesiredEndPercentage { get; set; } = null;

        
        public int? PortNumber { get; set; }
        public string? ChargingStationName { get; set; }
    }
}
