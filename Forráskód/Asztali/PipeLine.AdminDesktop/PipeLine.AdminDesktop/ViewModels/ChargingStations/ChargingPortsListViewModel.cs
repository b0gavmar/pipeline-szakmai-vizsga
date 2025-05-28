using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class ChargingPortsListViewModel : BaseViewModel
    {
        private readonly IChargingPortHttpService _chargingPortHttpService;

        public ChargingPortsListViewModel()
        {

            _chargingPortHttpService = new ChargingPortHttpService();


            _selectedChargingPort = new ChargingPort();
        }
        public ChargingPortsListViewModel(IChargingPortHttpService? chargingPortHttpService)
        {
            _chargingPortHttpService = chargingPortHttpService ?? throw new ArgumentNullException(nameof(chargingPortHttpService));
            _selectedChargingPort = new ChargingPort();
        }

        [ObservableProperty]
        private ChargingPort _newPort = new();
        [ObservableProperty]
        private ChargingPort _selectedChargingPort = new ChargingPort();
        [ObservableProperty]
        private ObservableCollection<ChargingPort> _chargingPorts = new();

        [RelayCommand]
        public async Task DoNewChargingPort()
        {
            var response = await _chargingPortHttpService.CreateAsync(NewPort);

            if (response is not null)
            {
                await UpdateAsync();
                SelectedChargingPort = NewPort;
            }
            await UpdateAsync();

        }

        [RelayCommand]
        public async Task DoRemove(ChargingPort chargingStationToDelete)
        {
            await _chargingPortHttpService.DeleteAsync(chargingStationToDelete.Id);
            await UpdateAsync();

        }

        [RelayCommand]
        public async Task DoUpdate(ChargingPort chargingStationToSave)
        {
            await _chargingPortHttpService.UpdateAsync(chargingStationToSave);
            await UpdateAsync();

        }
        private async Task UpdateAsync()
        {
            
            List<ChargingPort> ports = await _chargingPortHttpService.GetAllAsync();
            ChargingPorts = new ObservableCollection<ChargingPort>(ports);


        }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            await UpdateAsync();
        }
    }

}
