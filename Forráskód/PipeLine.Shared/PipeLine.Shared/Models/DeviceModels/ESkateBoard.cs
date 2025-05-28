using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models.DeviceModels
{
    public class ESkateBoard : Device
    {
        public bool CanBeLocked { get; set; } = false;

        public ESkateBoard(Guid id, string manufacturer, string model, string name, Guid applicationUserId, double batteryCapacity, double maxChargingSpeed, bool canBeLocked, double batteryVoltage) : base(id, manufacturer, model, name, applicationUserId, batteryCapacity, maxChargingSpeed, batteryVoltage)
        {
            CanBeLocked = canBeLocked;
            DeviceType = DeviceType.ESkateBoard;
        }

        public ESkateBoard(string manufacturer, string model, Guid applicationUserId) : base(manufacturer, model, applicationUserId)
        {
            DeviceType = DeviceType.ESkateBoard;
        }


        public ESkateBoard() : base()
        {
        }
    }
}
