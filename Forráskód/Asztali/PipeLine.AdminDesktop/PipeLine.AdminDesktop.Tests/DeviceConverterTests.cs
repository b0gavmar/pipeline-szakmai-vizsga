using System;
using Xunit;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Converters;
using PipeLine.Shared.Models.Enums;

namespace PipeLine.AdminDesktop.Tests
{
    public class DeviceConverterTests
    {
        [Fact]
        public void ToDto_FromEBike_ShouldMapAllFieldsCorrectly()
        {
            var ebike = new EBike
            {
                Id = Guid.NewGuid(),
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Name = "TestEBike",
                DeviceType = DeviceType.EBike,
                MaxChargingSpeed = 50,
                ApplicationUserId = Guid.NewGuid(),
                BatteryCapacity = 500,
                BatteryVoltage = 48,
                DetachableBattery = true
            };

            var dto = ebike.ToDto();

            Assert.Equal(ebike.Id, dto.Id);
            Assert.Equal(ebike.Manufacturer, dto.Manufacturer);
            Assert.Equal(ebike.Model, dto.Model);
            Assert.Equal(ebike.Name, dto.Name);
            Assert.Equal(ebike.DeviceType, dto.DeviceType);
            Assert.Equal(ebike.MaxChargingSpeed, dto.MaxChargingSpeed);
            Assert.Equal(ebike.ApplicationUserId, dto.ApplicationUserId);
            Assert.Equal(ebike.BatteryCapacity, dto.BatteryCapacity);
            Assert.Equal(ebike.BatteryVoltage, dto.BatteryVoltage);
            Assert.True(dto.DetachableBattery);
            Assert.Null(dto.IsFoldable);
            Assert.Null(dto.CanBeLocked);
        }

        [Fact]
        public void ToModel_FromEBikeDto_ShouldMapAllFieldsCorrectly()
        {
            var dto = new DeviceDto
            {
                Id = Guid.NewGuid(),
                Manufacturer = "TestManufacturer",
                Model = "TestModel",
                Name = "TestEBike",
                DeviceType = DeviceType.EBike,
                MaxChargingSpeed = 50,
                ApplicationUserId = Guid.NewGuid(),
                BatteryCapacity = 500,
                BatteryVoltage = 48,
                DetachableBattery = true
            };

            var device = dto.ToModel();

            var ebike = Assert.IsType<EBike>(device);
            Assert.True(ebike.DetachableBattery);
        }

        [Fact]
        public void ToDto_FromEScooter_ShouldMapAllFieldsCorrectly()
        {
            var escooter = new EScooter
            {
                Id = Guid.NewGuid(),
                Manufacturer = "ScooterCo",
                Model = "Scooty",
                Name = "FastScooter",
                DeviceType = DeviceType.EScooter,
                MaxChargingSpeed = 30,
                ApplicationUserId = Guid.NewGuid(),
                BatteryCapacity = 300,
                BatteryVoltage = 36,
                IsFoldable = true
            };

            var dto = escooter.ToDto();

            Assert.Equal(escooter.IsFoldable, dto.IsFoldable);
            Assert.Null(dto.DetachableBattery);
            Assert.Null(dto.CanBeLocked);
        }

        [Fact]
        public void ToModel_FromEScooterDto_ShouldMapAllFieldsCorrectly()
        {
            var dto = new DeviceDto
            {
                Id = Guid.NewGuid(),
                Manufacturer = "ScooterCo",
                Model = "Scooty",
                Name = "FastScooter",
                DeviceType = DeviceType.EScooter,
                MaxChargingSpeed = 30,
                ApplicationUserId = Guid.NewGuid(),
                BatteryCapacity = 300,
                BatteryVoltage = 36,
                IsFoldable = true
            };

            var device = dto.ToModel();

            var escooter = Assert.IsType<EScooter>(device);
            Assert.True(escooter.IsFoldable);
        }

        [Fact]
        public void ToDto_FromESkateBoard_ShouldMapAllFieldsCorrectly()
        {
            var eskateBoard = new ESkateBoard
            {
                Id = Guid.NewGuid(),
                Manufacturer = "BoardInc",
                Model = "Skaty",
                Name = "Boardy",
                DeviceType = DeviceType.ESkateBoard,
                MaxChargingSpeed = 25,
                ApplicationUserId = Guid.NewGuid(),
                BatteryCapacity = 200,
                BatteryVoltage = 24,
                CanBeLocked = true
            };

            var dto = eskateBoard.ToDto();

            Assert.Equal(eskateBoard.CanBeLocked, dto.CanBeLocked);
            Assert.Null(dto.IsFoldable);
            Assert.Null(dto.DetachableBattery);
        }

        [Fact]
        public void ToModel_FromESkateBoardDto_ShouldMapAllFieldsCorrectly()
        {
            var dto = new DeviceDto
            {
                Id = Guid.NewGuid(),
                Manufacturer = "BoardInc",
                Model = "Skaty",
                Name = "Boardy",
                DeviceType = DeviceType.ESkateBoard,
                MaxChargingSpeed = 25,
                ApplicationUserId = Guid.NewGuid(),
                BatteryCapacity = 200,
                BatteryVoltage = 24,
                CanBeLocked = true
            };

            var device = dto.ToModel();

            var eskateboard = Assert.IsType<ESkateBoard>(device);
            Assert.True(eskateboard.CanBeLocked);
        }

        [Fact]
        public void ToModel_FromEmptyDeviceDto_ShouldReturnBasicDevice()
        {
            var dto = new DeviceDto
            {
                Id = Guid.NewGuid(),
                Manufacturer = "GenericCo",
                Model = "ModelX",
                Name = "GenericDevice",
                DeviceType = DeviceType.Empty,
                MaxChargingSpeed = 10,
                ApplicationUserId = Guid.NewGuid(),
                BatteryCapacity = 100,
                BatteryVoltage = 12
            };

            var device = dto.ToModel();

            Assert.IsType<Device>(device);
        }
    }
}
