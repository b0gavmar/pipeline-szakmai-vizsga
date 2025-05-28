using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.Enums;
using PipeLine.Shared.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PipeLine.Shared.Models.DeviceModels
{

    public class Device : IDbEntity<Device>
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

        public ApplicationUser ApplicationUser { get; set; }
        public List<ChargingInstance> ChargingInstances { get; set;}

        public Device(Guid id, string manufacturer, string model, string name, Guid applicationUserId,double batteryCapacity, double maxChargingSpeed, double batteryVoltage)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Name = name;
            ApplicationUserId = applicationUserId;
            BatteryCapacity = batteryCapacity;
            MaxChargingSpeed = maxChargingSpeed;
            ApplicationUserId = applicationUserId;
            BatteryVoltage = batteryVoltage;
        }

        public Device(string manufacturer, string model, string name, Guid applicationUserId, double batteryCapacity, double maxChargingSpeed, double batteryVoltage)
        {
            Id = Guid.NewGuid();
            Manufacturer = manufacturer;
            Model = model;
            if(name == null)
            {
                Name = manufacturer + " " + model;
            }
            Name = name;
            ApplicationUserId = applicationUserId;
            BatteryCapacity = batteryCapacity;
            MaxChargingSpeed = maxChargingSpeed;
            BatteryVoltage = batteryVoltage;
        }

        public Device(string manufacturer, string model, Guid applicationUserId)
        {
            Id = Guid.NewGuid();
            Manufacturer = manufacturer;
            Model = model;
            Name = manufacturer + " " + model;
            ApplicationUserId = applicationUserId;
        }

        public Device()
        {
        }

        public override string ToString()
        {
            return $"Device ID: {Id}\n" +
                   $"Device Type: {DeviceType}\n" +
                   $"Manufacturer: {Manufacturer}\n" +
                   $"Model: {Model}\n" +
                   $"Name: {Name}\n" +
                   $"Application User ID: {ApplicationUserId}\n" +
                   $"Battery Capacity: {BatteryCapacity} mAh\n" +
                   $"Max Charging Speed: {MaxChargingSpeed} kWh\n" +
                   $"Battery Voltage: {BatteryVoltage} V";
        }

    }
}
