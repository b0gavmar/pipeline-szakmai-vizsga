using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FontAwesome.Sharp;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.ViewModels.ChargingInstances;
using PipeLine.AdminDesktop.ViewModels.ChargingStations;
using PipeLine.AdminDesktop.ViewModels.ControlPanel;
using PipeLine.AdminDesktop.ViewModels.Devices;
using PipeLine.AdminDesktop.ViewModels.ErrorTickets2;
using PipeLine.AdminDesktop.ViewModels.Users;
using PipeLine.Shared.Models.ChargingStationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels
{
    public partial class MainViewModel: BaseViewModel
    {
        private ChargingStationsViewModel _chargingStationsViewModel;
        private ControlPanelViewModel _controlPanelViewModel;
        private DevicesViewModel _devicesViewModel;
        private UsersViewModel _usersViewModel;
        private ErrorTicketsViewModel2 _errorTicketsViewModel;
        private ChargingInstancesViewModel _chargingInstancesViewModel;

        public MainViewModel()
        {
            _chargingStationsViewModel = new ChargingStationsViewModel();
            _controlPanelViewModel = new ControlPanelViewModel();
            _devicesViewModel = new DevicesViewModel();
            _usersViewModel = new UsersViewModel();
            _errorTicketsViewModel = new ErrorTicketsViewModel2();
            _chargingInstancesViewModel = new ChargingInstancesViewModel();

            CurrentChildView = _controlPanelViewModel;
        }

        public MainViewModel(ControlPanelViewModel controlPanelViewModel,
            DevicesViewModel devicesViewModel,
            ChargingStationsViewModel chargingStationsViewModel,
            UsersViewModel usersViewModel, 
            ErrorTicketsViewModel2 errorTicketsViewModel,
            ChargingInstancesViewModel chargingInstancesViewModel)
        {
            _controlPanelViewModel = controlPanelViewModel;
            _devicesViewModel = devicesViewModel;
            _chargingStationsViewModel = chargingStationsViewModel;
             _usersViewModel = usersViewModel;
            _errorTicketsViewModel = errorTicketsViewModel;
            _chargingInstancesViewModel = chargingInstancesViewModel;

            CurrentChildView = _controlPanelViewModel;
            ShowDashboard();
        }

        [ObservableProperty]
        private string _caption = string.Empty;

        [ObservableProperty]
        private IconChar _icon = new IconChar();

        [ObservableProperty]
        private BaseViewModel? _currentChildView;

        [RelayCommand]
        public async Task ShowDashboard()
        {
            await _controlPanelViewModel.InitializeAsync();
            Caption = "Home page";
            Icon = IconChar.House;
            CurrentChildView = _controlPanelViewModel;
        }
        [RelayCommand]
        public async Task ShowDevices()
        {
            await _devicesViewModel.ShowUnifiedDeviceView();
            Caption = "Devices";
            Icon = IconChar.Bicycle;
            CurrentChildView = _devicesViewModel;
        }
        [RelayCommand]
        public async Task ShowUsers()
        {
            await _usersViewModel.ShowUnifiedApplicationUserView();
            Caption = "Users";
            Icon = IconChar.Person;
            CurrentChildView = _usersViewModel;
        }

        [RelayCommand]
        public async Task ShowChargingStations()
        {
            await _chargingStationsViewModel.ShowUnifiedChargingStationView();
            Caption = "Charging Stations";
            Icon = IconChar.ChargingStation;
            CurrentChildView = _chargingStationsViewModel;
        }

        [RelayCommand]
        public async Task ShowErrorTickets()
        {
            await _errorTicketsViewModel.ShowErrorTicketsListView();
            Caption = "Error Tickets";
            Icon = IconChar.ExclamationCircle;
            CurrentChildView = _errorTicketsViewModel;
        }

        [RelayCommand]
        public async Task ShowChargingInstances()
        {
            await _chargingInstancesViewModel.ShowChargingInstancesListView();
            Caption = "Charging Instances";
            Icon = IconChar.History;
            CurrentChildView = _chargingInstancesViewModel;
        }

    }
}
