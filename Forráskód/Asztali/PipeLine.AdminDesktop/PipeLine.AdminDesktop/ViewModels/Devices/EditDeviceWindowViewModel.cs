using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.Views.Devices;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipeLine.AdminDesktop.ViewModels.Devices
{
    public partial class EditDeviceWindowViewModel: BaseViewModel
    {
        private readonly IDeviceHttpService _deviceHttpService = new DeviceHttpService();
        public IEnumerable<DeviceType> DeviceTypes => Enum.GetValues<DeviceType>();


        [ObservableProperty]
        private Device _device;

        [ObservableProperty]
        public bool _additionalProperty = false;
        [ObservableProperty]
        public string _additionalPropText = ":";

        public EditDeviceWindowViewModel(Device device)
        {
            Device = device;
        }

        [RelayCommand]
        public async Task DoUpdate(Window window)
        {
            await _deviceHttpService.UpdateAsync(Device);
            window.Close();
        }

        public async override Task InitializeAsync()
        {
            SetPropText();
            await base.InitializeAsync();
        }

        private void SetPropText()
        {
            if (Device.DeviceType == DeviceType.EBike)
            {
                AdditionalPropText = "Detachable Battery:";
            }
            else if (Device.DeviceType == DeviceType.EScooter)
            {
                AdditionalPropText = "Is Foldable:";
            }
            else if (Device.DeviceType == DeviceType.ESkateBoard)
            {
                AdditionalPropText = "Can Be Locked:";
            }
            else
            {

            }
        }
    }
}
