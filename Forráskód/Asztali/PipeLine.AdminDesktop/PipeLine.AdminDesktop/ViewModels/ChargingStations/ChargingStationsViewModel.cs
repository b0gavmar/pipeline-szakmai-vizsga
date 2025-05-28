using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.ViewModels.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class ChargingStationsViewModel : BaseViewModel
    {
        public ChargingStationsListViewModel _chargingStationsListViewModel;
        public ChargingPortsListViewModel _chargingPortsListViewModel;
        public UnifiedChargingStationViewModel _unifiedChargingStationViewModel;
        public AddNewChargingStationViewModel _addNewChargingStationViewModel;
        public AddNewChargingPortViewModel _addNewChargingPortViewModel;

        public ChargingStationsViewModel()
        {
            _chargingStationsListViewModel = new ChargingStationsListViewModel();
            _chargingPortsListViewModel = new ChargingPortsListViewModel();
            _unifiedChargingStationViewModel = new UnifiedChargingStationViewModel();
            _addNewChargingStationViewModel = new AddNewChargingStationViewModel();
            _addNewChargingPortViewModel = new AddNewChargingPortViewModel();
            _currentChargingStationChildView = _unifiedChargingStationViewModel;
        }
        public ChargingStationsViewModel(ChargingStationsListViewModel chargingStationsListViewModel, ChargingPortsListViewModel chargingPortsListViewModel,
            UnifiedChargingStationViewModel unifiedChargingStationViewModel,AddNewChargingStationViewModel addNewChargingStationViewModel, 
            AddNewChargingPortViewModel addNewChargingPortViewModel)
        {
            _chargingStationsListViewModel = chargingStationsListViewModel;
            _chargingPortsListViewModel = chargingPortsListViewModel;
            _unifiedChargingStationViewModel = unifiedChargingStationViewModel;
            _addNewChargingStationViewModel = addNewChargingStationViewModel;
            _addNewChargingPortViewModel = addNewChargingPortViewModel;
            _currentChargingStationChildView = unifiedChargingStationViewModel;
        }

        [ObservableProperty]
        private BaseViewModel _currentChargingStationChildView;

        [RelayCommand]
        public async Task ShowChargingStationsListView()
        {
            await _chargingStationsListViewModel.InitializeAsync();
            CurrentChargingStationChildView = _chargingStationsListViewModel;
        }
        [RelayCommand]
        public async Task ShowChargingPortsListView()
        {
            await _chargingPortsListViewModel.InitializeAsync();
            CurrentChargingStationChildView = _chargingPortsListViewModel;
        }

        [RelayCommand]
        public async Task ShowUnifiedChargingStationView()
        {
            await _unifiedChargingStationViewModel.InitializeAsync();
            CurrentChargingStationChildView = _unifiedChargingStationViewModel;
        }
        
        [RelayCommand]
        public async Task ShowAddNewChargingStationView()
        {
            await _addNewChargingStationViewModel.InitializeAsync();
            CurrentChargingStationChildView = _addNewChargingStationViewModel;
        }
        [RelayCommand]
        public async Task ShowAddNewChargingPortView()
        {
            await _addNewChargingPortViewModel.InitializeAsync();
            CurrentChargingStationChildView = _addNewChargingPortViewModel;
        }
        
    }
}
