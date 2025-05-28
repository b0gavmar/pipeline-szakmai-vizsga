using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models.DeviceModels
{
    public class EBike : Device
    {
        public bool DetachableBattery { get; set; } = false;

        public EBike(Guid id, string manufacturer, string model, string name, Guid applicationUserId, double batteryCapacity, double maxChargingSpeed, bool detachableBattery, double batteryVoltage) : base(id, manufacturer, model, name, applicationUserId, batteryCapacity, maxChargingSpeed, batteryVoltage)
        {
            DetachableBattery = detachableBattery;
            DeviceType = DeviceType.EBike;
        }

        public EBike(string manufacturer, string model, Guid applicationUserId) : base(manufacturer, model, applicationUserId)
        {
            DeviceType = DeviceType.EBike;
        }


        public EBike() : base()
        {
        }

    }
}
