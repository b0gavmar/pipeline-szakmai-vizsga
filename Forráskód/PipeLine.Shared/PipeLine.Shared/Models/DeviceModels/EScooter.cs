using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models.DeviceModels
{
    public class EScooter : Device
    {
        public bool IsFoldable { get; set; } = false;

        public EScooter(Guid id, string manufacturer, string model, string name, Guid applicationUserId, double batteryCapacity, double maxChargingSpeed, bool isFoldable, double batteryVoltage) : base(id, manufacturer, model, name, applicationUserId, batteryCapacity, maxChargingSpeed, batteryVoltage)
        {
            IsFoldable = isFoldable;
            DeviceType = DeviceType.EScooter;
        }

        public EScooter(string manufacturer, string model, Guid applicationUserId) : base(manufacturer, model, applicationUserId)
        {
            DeviceType = DeviceType.EScooter;
        }

        public EScooter() : base()
        {
        }
    }
}
