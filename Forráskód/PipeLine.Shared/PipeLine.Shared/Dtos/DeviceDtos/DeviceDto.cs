using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Dtos.DeviceDtos
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public DeviceType DeviceType { get; set; } = DeviceType.Empty;
        public string? Manufacturer { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid ApplicationUserId { get; set; }
        public double? BatteryCapacity { get; set; }
        public double? MaxChargingSpeed { get; set; }
        public double? BatteryVoltage { get; set; }


        public bool? DetachableBattery { get; set; }
        public bool? IsFoldable { get; set; }
        public bool? CanBeLocked { get; set; }
    }
}
