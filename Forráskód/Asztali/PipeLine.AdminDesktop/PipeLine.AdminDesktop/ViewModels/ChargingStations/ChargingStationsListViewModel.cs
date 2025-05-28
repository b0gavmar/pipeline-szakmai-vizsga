using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.Repos;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.Backend.Assemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class ChargingStationsListViewModel : BaseViewModel
    {
        private readonly IChargingStationHttpService _chargingStationHttpService;

        public ChargingStationsListViewModel()
        {

            _chargingStationHttpService = new ChargingStationHttpService();


            _selectedChargingStation = new ChargingStation();
        }
        public ChargingStationsListViewModel(IChargingStationHttpService? chargingStationHttpService, IChargingPortHttpService? chargingPortHttpService)
        {
            _chargingStationHttpService = chargingStationHttpService ?? throw new ArgumentNullException(nameof(chargingStationHttpService));
            _selectedChargingStation = new ChargingStation();
        }

        [ObservableProperty]
        private ChargingStation _newstation = new();
        [ObservableProperty]
        private ChargingStation _selectedChargingStation = new ChargingStation();
        [ObservableProperty]
        private ObservableCollection<ChargingStation> _chargingStations = new();

        [RelayCommand]
        public async Task DoNewChargingStation()
        {
            var response = await _chargingStationHttpService.CreateAsync(Newstation);

            if (response is not null)
            {
                SelectedChargingStation = Newstation; 
                await UpdateAsync();
            }
            await UpdateAsync();
           
        }

        [RelayCommand]
        public async Task DoRemove(ChargingStation chargingStationToDelete)
        {
            await _chargingStationHttpService.DeleteAsync(chargingStationToDelete.Id);
            await UpdateAsync();

        }

        [RelayCommand]
        public async Task DoUpdate(ChargingStation chargingStationToSave)
        {
            await _chargingStationHttpService.UpdateAsync(chargingStationToSave);
            await UpdateAsync();

        }
        private async Task UpdateAsync()
        {
            List<ChargingStation> stations = await _chargingStationHttpService.GetAllAsync();
            ChargingStations = new ObservableCollection<ChargingStation>(stations);


        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await UpdateAsync();
        }
    }
}
