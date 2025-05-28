using Xunit;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PipeLine.AdminDesktop.ViewModels.Devices;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.HttpService;
using PipeLine.Backend.Assemblers.DeviceAssemblers;

namespace PipeLine.AdminDesktop.Tests
{
    public class UnifiedDeviceViewModelTests
    {
        [Fact]
        public async Task UpdateAsync_ShouldCallGetAllAsync()
        {
            var mockDeviceHttpService = new Mock<IDeviceHttpService>();
            var mockDeviceAssembler = new Mock<IDeviceAssembler>();

            var devicesFromApi = new List<Device>
        {
            new Device { Id = Guid.NewGuid(), Name = "Device A" },
            new Device { Id = Guid.NewGuid(), Name = "Device B" }
        };

            mockDeviceHttpService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(devicesFromApi);

            var viewModel = new UnifiedDeviceViewModel(mockDeviceHttpService.Object, mockDeviceAssembler.Object);

            await viewModel.InitializeAsync();

            mockDeviceHttpService.Verify(s => s.GetAllAsync(), Times.Once);
            Assert.Equal(2, viewModel.Devices.Count);
        }
    }
}
