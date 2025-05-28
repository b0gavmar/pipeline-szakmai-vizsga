using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class EditChargingStationWindowViewModel : BaseViewModel
    {
        private readonly IChargingStationHttpService _chargingStationHttpService;

        [ObservableProperty]
        private ChargingStation _chargingStation;


        public EditChargingStationWindowViewModel(ChargingStation chargingStation, IChargingStationHttpService chargingStationHttpService)
        {
            ChargingStation = chargingStation;
            _chargingStationHttpService = chargingStationHttpService;
        }

        [RelayCommand]
        public async Task DoUpdateStation(Window window)
        {
            await _chargingStationHttpService.UpdateAsync(ChargingStation);
            window.Close();
        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
