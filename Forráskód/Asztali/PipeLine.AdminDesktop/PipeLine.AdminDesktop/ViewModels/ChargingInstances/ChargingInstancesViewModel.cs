using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.Views.ChargingStations;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ChargingInstanceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ChargingInstances
{
    public partial class ChargingInstancesViewModel : BaseViewModel
    {
        private ChargingInstancesListViewModel _chargingInstancesListViewModel;

        public ChargingInstancesViewModel()
        {
            _chargingInstancesListViewModel = new ChargingInstancesListViewModel();

            _currentChildViewModel = _chargingInstancesListViewModel;
        }

        public ChargingInstancesViewModel(ChargingInstancesListViewModel chargingInstancesListViewModel)
        {
            _chargingInstancesListViewModel = chargingInstancesListViewModel;
            _currentChildViewModel = _chargingInstancesListViewModel;
        }

        [ObservableProperty]
        private BaseViewModel _currentChildViewModel;

        [RelayCommand]
        public async Task ShowChargingInstancesListView()
        {
            await _chargingInstancesListViewModel.InitializeAsync();
            CurrentChildViewModel = _chargingInstancesListViewModel;
        }
    }
}
