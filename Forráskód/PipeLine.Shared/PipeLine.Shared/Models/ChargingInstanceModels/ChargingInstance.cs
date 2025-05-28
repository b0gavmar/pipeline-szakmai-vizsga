using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.DeviceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models.ChargingInstanceModels
{
    public class ChargingInstance : IDbEntity<ChargingInstance>
    {
        public Guid Id { get; set; }
        public Guid? ChargingPortId { get; set; }
        public Guid? DeviceId { get; set; }
        public DateTime? Start {  get; set; }
        public DateTime? End { get; set; } = null;
        public int? StartingPercentage { get; set; } = null;
        public int? EndPercentage { get; set; } = null;
        public int? DesiredEndPercentage { get; set; } = null;

        public Device? Device { get; set; } 
        public ChargingPort? ChargingPort { get; set; }

        public ChargingInstance() 
        {
        }

        public ChargingInstance (Guid id, Guid chargingPortId, Guid deviceId, DateTime start, DateTime end, int startingPercentage, int endPercentage, int desiredEndPercentage)
        {
            Id = id;
            ChargingPortId = chargingPortId;
            DeviceId = deviceId;
            Start = start;
            End = end;
            StartingPercentage = startingPercentage;
            EndPercentage = endPercentage;
            DesiredEndPercentage = desiredEndPercentage;
        }


        public override string ToString()
        {
            return $"Instance ID: {Id} \n Port ID: {ChargingPortId} \n Device ID: {DeviceId} \n Start of session: {Start} \n End of session: {End}";
        }
    }
}
