using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;

namespace PipeLine.Shared.Converters
{
    public static class DeviceConverter
    {
        public static DeviceDto ToDto(this Device device)
        {
            DeviceDto dto = new DeviceDto
            {
                Id = device.Id,
                Manufacturer = device.Manufacturer,
                Model = device.Model,
                Name = device.Name,
                DeviceType = device.DeviceType,
                MaxChargingSpeed = device.MaxChargingSpeed,
                ApplicationUserId = device.ApplicationUserId,
                BatteryCapacity = device.BatteryCapacity,
                BatteryVoltage = device.BatteryVoltage
            };

            switch (device)
            {
                case EBike ebike:
                    dto.DetachableBattery = ebike.DetachableBattery;
                    break;

                case EScooter escooter:
                    dto.IsFoldable = escooter.IsFoldable;
                    break;

                case ESkateBoard eSkateBoard:
                    dto.CanBeLocked = eSkateBoard.CanBeLocked;
                    break;
            }

            return dto;
        }

        public static Device ToModel(this DeviceDto dto)
        {
            return dto.DeviceType switch
            {
                DeviceType.EBike => new EBike
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Name = dto.Name,
                    DeviceType = dto.DeviceType,
                    MaxChargingSpeed = dto.MaxChargingSpeed,
                    ApplicationUserId = dto.ApplicationUserId,
                    BatteryCapacity = dto.BatteryCapacity,
                    BatteryVoltage = dto.BatteryVoltage,
                    DetachableBattery = dto.DetachableBattery ?? false
                },
                DeviceType.EScooter => new EScooter
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Name = dto.Name,
                    DeviceType = dto.DeviceType,
                    MaxChargingSpeed = dto.MaxChargingSpeed,
                    ApplicationUserId = dto.ApplicationUserId,
                    BatteryCapacity = dto.BatteryCapacity,
                    BatteryVoltage = dto.BatteryVoltage,
                    IsFoldable = dto.IsFoldable ?? false
                },
                DeviceType.ESkateBoard => new ESkateBoard
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Name = dto.Name,
                    DeviceType = dto.DeviceType,
                    MaxChargingSpeed = dto.MaxChargingSpeed,
                    ApplicationUserId = dto.ApplicationUserId,
                    BatteryCapacity = dto.BatteryCapacity,
                    BatteryVoltage = dto.BatteryVoltage,
                    CanBeLocked = dto.CanBeLocked ?? false
                },
                DeviceType.Empty => new Device
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Name = dto.Name,
                    DeviceType = dto.DeviceType,
                    MaxChargingSpeed = dto.MaxChargingSpeed,
                    ApplicationUserId = dto.ApplicationUserId,
                    BatteryCapacity = dto.BatteryCapacity,
                    BatteryVoltage = dto.BatteryVoltage
                },
                _ => throw new NotImplementedException()
            };
        }
    }
}
