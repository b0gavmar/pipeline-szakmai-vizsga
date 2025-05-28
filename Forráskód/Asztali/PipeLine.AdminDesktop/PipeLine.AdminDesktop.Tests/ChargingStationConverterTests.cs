using System;
using Xunit;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Converters.ChargingStationConverters;

namespace PipeLine.AdminDesktop.Tests
{
    public class ChargingStationConverterTests
    {
        [Fact]
        public void ToDto_FromChargingStation_ShouldMapAllFieldsCorrectly()
        {
            var station = new ChargingStation
            {
                Id = Guid.NewGuid(),
                Name = "TestStation",
                Latitude = 47.4979,
                Longitude = 19.0402,
                Address = "Budapest, Hungary"
            };

            var dto = station.ToDto();

            Assert.Equal(station.Id, dto.Id);
            Assert.Equal(station.Name, dto.Name);
            Assert.Equal(station.Latitude, dto.Latitude);
            Assert.Equal(station.Longitude, dto.Longitude);
            Assert.Equal(station.Address, dto.Address);
        }

        [Fact]
        public void ToModel_FromChargingStationDto_ShouldMapAllFieldsCorrectly()
        {
            var dto = new ChargingStationDto
            {
                Id = Guid.NewGuid(),
                Name = "TestStation",
                Latitude = 47.4979,
                Longitude = 19.0402,
                Address = "Budapest, Hungary"
            };

            var station = dto.ToModel();

            Assert.Equal(dto.Id, station.Id);
            Assert.Equal(dto.Name, station.Name);
            Assert.Equal(dto.Latitude, station.Latitude);
            Assert.Equal(dto.Longitude, station.Longitude);
            Assert.Equal(dto.Address, station.Address);
        }
    }
}
