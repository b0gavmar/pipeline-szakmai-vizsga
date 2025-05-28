using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.Views.Devices;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.DeviceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.Devices
{
    public partial class UnifiedDeviceViewModel:BaseViewModel
    {
        private readonly IDeviceHttpService _deviceHttpService;
        private readonly IDeviceAssembler _deviceAssembler;

        [ObservableProperty]
        private ObservableCollection<Device> _devices = new ();

        [ObservableProperty]
        private Device _selectedDevice = new Device();

        public UnifiedDeviceViewModel()
        {
            _deviceHttpService = new DeviceHttpService();
            _deviceAssembler = new DeviceAssembler();
            _selectedDevice = new Device();
        }

        public UnifiedDeviceViewModel(IDeviceHttpService? deviceHttpService, IDeviceAssembler? deviceAssembler)
        {
            _deviceHttpService = deviceHttpService ?? throw new ArgumentNullException(nameof(deviceHttpService));
            _deviceAssembler = deviceAssembler ?? throw new ArgumentNullException(nameof(deviceAssembler));
            _selectedDevice = new Device();
        }

        [ObservableProperty]
        private string _searchText = string.Empty;


        [RelayCommand]
        public async Task SearchDevices()
        {
            

            UpdateAsync();
        }

        [RelayCommand]
        public async Task DoRemove(Device device)
        {
            await _deviceHttpService.DeleteAsync(device.Id);
            await UpdateAsync();
        }

        [RelayCommand]
        public async Task DoUpdate(Device device)
        {
            var editWindow = new EditDeviceWindowView
            {
                DataContext = new EditDeviceWindowViewModel(device)
            };
            editWindow.ShowDialog();

            await _deviceHttpService.UpdateAsync(device);
            await UpdateAsync();
        }

        public override async Task InitializeAsync()
        {
            await UpdateAsync();
            await base.InitializeAsync();
        }

        private async Task UpdateAsync()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                var filteredDevices = await _deviceHttpService.GetFilteredDevicesAsync(SearchText);
                Devices = new ObservableCollection<Device>(_deviceAssembler.ToModels(filteredDevices));
            }
            else
            {
                Devices = new ObservableCollection<Device>(await _deviceHttpService.GetAllAsync());
            }

        }
    }
}
