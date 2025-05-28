using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.ChargingStationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class EditChargingPortWindowViewModel : BaseViewModel
    {
        private readonly IChargingPortHttpService _chargingPortHttpService;
        private readonly IChargingStationHttpService _chargingStationHttpService;

        [ObservableProperty]
        private ChargingPort _chargingPort;



        public EditChargingPortWindowViewModel(ChargingPort chargingPort, IChargingPortHttpService? chargingPortHttpService)
        {
            ChargingPort = chargingPort;
            _chargingPortHttpService = chargingPortHttpService ?? throw new ArgumentNullException(nameof(chargingPortHttpService));
        }

        [RelayCommand]
        public async Task DoUpdatePort(Window window)
        {
            await _chargingPortHttpService.UpdateAsync(ChargingPort);
            window.Close();
        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
