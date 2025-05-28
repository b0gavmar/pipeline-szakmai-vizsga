using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Models.ChargingStationModels
{
    public class ChargingStation : IDbEntity<ChargingStation>
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = string.Empty;
        public List<ChargingPort> Ports { get; set; }
        public List<ErrorTicket> ErrorTickets { get; set; }

        public ChargingStation(Guid id, string name, double latitude, double longitude, string address,List<ChargingPort> ports)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            Ports = ports;
        }

        public ChargingStation()
        {

        }
        public override string ToString() 
        { 
            return $"ID: {Id} \n Name: {Name} \n Address: {Address} \n Longitude: {Longitude} \n Latitude: {Latitude}";
        }
    }
}
