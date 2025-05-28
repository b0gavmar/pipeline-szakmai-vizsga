using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.Devices
{
    public partial class DevicesViewModel: BaseViewModel
    {

        public UnifiedDeviceViewModel _unifiedDeviceViewModel;
        public AddNewDeviceViewModel _addNewDeviceViewModel;

        public DevicesViewModel()
        {
            _unifiedDeviceViewModel = new UnifiedDeviceViewModel();
            _addNewDeviceViewModel = new AddNewDeviceViewModel();

            _currentDeviceChildView = new UnifiedDeviceViewModel();
        }

        public DevicesViewModel(UnifiedDeviceViewModel unifiedDeviceViewModel, AddNewDeviceViewModel addNewDeviceViewModel)
        {

            _unifiedDeviceViewModel = unifiedDeviceViewModel;
            _addNewDeviceViewModel = addNewDeviceViewModel;

            _currentDeviceChildView = unifiedDeviceViewModel;
        }

        [ObservableProperty]
        private BaseViewModel _currentDeviceChildView;


        [RelayCommand]
        public async Task ShowUnifiedDeviceView()
        {
            await _unifiedDeviceViewModel.InitializeAsync();
            CurrentDeviceChildView = _unifiedDeviceViewModel;
        }

        [RelayCommand]
        public async Task ShowAddNewDeviceView()
        {
            await _addNewDeviceViewModel.InitializeAsync();
            CurrentDeviceChildView = _addNewDeviceViewModel;
        }

    }
}
