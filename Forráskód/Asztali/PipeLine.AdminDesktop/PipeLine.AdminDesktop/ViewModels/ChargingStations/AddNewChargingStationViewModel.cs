using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class AddNewChargingStationViewModel : BaseViewModel
    {
        private readonly IChargingStationHttpService _chargingStationHttpService;
        private readonly IChargingStationAssembler _chargingStationAssembler;

        public AddNewChargingStationViewModel()
        {
            _chargingStationHttpService = new ChargingStationHttpService();
            _chargingStationAssembler = new ChargingStationAssembler();
        }

        public AddNewChargingStationViewModel(IChargingStationHttpService? chargingStationHttpService, IChargingStationAssembler chargingStationAssembler)
        {
            _chargingStationHttpService = chargingStationHttpService ?? throw new ArgumentNullException(nameof(chargingStationHttpService));
            _chargingStationAssembler = chargingStationAssembler ?? throw new ArgumentNullException(nameof(chargingStationAssembler));
        }

        [ObservableProperty]
        private string _warningMsg = string.Empty;

        [ObservableProperty]
        private ChargingStation _newChargingStation = new();

        [RelayCommand]
        public async Task AddNewChargingStation()
        {
            if(string.IsNullOrEmpty(NewChargingStation.Name) || ( NewChargingStation.Latitude == 0.0 && NewChargingStation.Longitude == 0.0 ))
            {
                WarningMsg = "Fill out every field";
            }
            else
            {
                await _chargingStationHttpService.CreateAsync(NewChargingStation);
                WarningMsg = "Created successfully";
            }

        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
