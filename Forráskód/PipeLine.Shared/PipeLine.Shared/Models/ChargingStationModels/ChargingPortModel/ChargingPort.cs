using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel
{
    public class ChargingPort : IDbEntity<ChargingPort>
    {
        public Guid Id { get; set; }
        public bool IsBeingUsed { get; set; }
        public bool IsDisabled { get; set; }
        public Guid ChargingStationId { get; set; }
        public ChargingStation ChargingStation { get; set; }
        public int PortNumber { get; set; }
        public double MaxChargingSpeed { get; set; }
        public bool IsCharging { get; set; }
        public List<ChargingInstance> ChargingInstances { get; set; }

        public ChargingPort(Guid id, bool isBeingUsed, bool isDisabled, ChargingStation chargingStation, Guid chargingStationId, int portNumber, double maxChargingSpeed, bool isCharging)
        {
            Id = id;
            IsBeingUsed = isBeingUsed;
            IsDisabled = isDisabled;
            ChargingStation = chargingStation;
            ChargingStationId = chargingStationId;
            PortNumber = portNumber;
            MaxChargingSpeed = maxChargingSpeed;
            IsCharging = isCharging;
        }
        public ChargingPort()
        {
        }

        public override string ToString()
        {
            return $" Own ID: {Id}, \n Is it enabled?:  {(IsDisabled ? "No": "Yes")}, \n Is it in use?: {(IsBeingUsed ? "Yes":"No")}";
        
        }


    }
}
