using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Standard;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.Views.ChargingStations;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class UnifiedChargingStationViewModel : BaseViewModel
    {
        private readonly IChargingStationHttpService _chargingStationHttpService;
        private readonly IChargingPortHttpService _chargingPortHttpService;
        private readonly IChargingStationAssembler _chargingStationAssembler;
        private readonly IChargingPortAssembler _chargingPortAssembler;

        [ObservableProperty]
        private ObservableCollection<ChargingStation> _chargingStations = new();

        [ObservableProperty]
        private ObservableCollection<ChargingPort> _chargingPorts = new();

        [ObservableProperty]
        private ChargingStation _selectedChargingStation = new();

        [ObservableProperty]
        private ChargingPort _selectedChargingPort = new();

        public UnifiedChargingStationViewModel()
        {
            _chargingStationHttpService = new ChargingStationHttpService();
            _chargingPortHttpService = new ChargingPortHttpService();
            _selectedChargingStation = new ChargingStation();
            _selectedChargingPort = new ChargingPort();
            _chargingStationAssembler = new ChargingStationAssembler();
            _chargingPortAssembler = new ChargingPortAssembler();
        }

        public UnifiedChargingStationViewModel(IChargingStationHttpService? chargingStationHttpService, IChargingPortHttpService? chargingPortHttpService, IChargingStationAssembler? chargingStationAssembler, IChargingPortAssembler? chargingPortAssembler)
        {
            _chargingStationHttpService = chargingStationHttpService ?? throw new ArgumentNullException(nameof(chargingStationHttpService));
            _chargingPortHttpService = chargingPortHttpService ?? throw new ArgumentNullException(nameof(chargingPortHttpService));
            _chargingStationAssembler = chargingStationAssembler ?? throw new ArgumentNullException(nameof(chargingStationAssembler));
            _chargingPortAssembler = chargingPortAssembler ?? throw new ArgumentNullException(nameof(chargingPortAssembler));
            _selectedChargingStation = new ChargingStation();
            _selectedChargingPort = new ChargingPort();
            //_ = InitializeAsync();
        }

        [ObservableProperty]
        private string _searchText = string.Empty;


        [RelayCommand]
        public async Task SearchStation()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ChargingStations = new ObservableCollection<ChargingStation>(await _chargingStationHttpService.GetAllAsync());
            }
            else
            {
                var filteredStations = await _chargingStationHttpService.GetFilteredChargingStationsAsync(SearchText);
                ChargingStations = new ObservableCollection<ChargingStation>(_chargingStationAssembler.ToModels(filteredStations));
            }
            UpdateAsync();
        }

        [RelayCommand]
        public async Task SearchPortsOfStation()
        {
            if (SelectedChargingStation != null)
            {
                var filteredStations = await _chargingPortHttpService.GetChargingPortsOfStationsAsync(SelectedChargingStation.Id);
                ChargingPorts = new ObservableCollection<ChargingPort>(_chargingPortAssembler.ToModels(filteredStations));
            }
            else
            {
                ChargingPorts = new ObservableCollection<ChargingPort>();
            }

            UpdateAsync();
        }


        [RelayCommand]
        public async Task DoRemoveStation(ChargingStation chargingStation)
        {
            await _chargingStationHttpService.DeleteAsync(chargingStation.Id);
            await UpdateAsync();
        }

        [RelayCommand]
        public async Task DoRemovePort(ChargingPort chargingPort)
        {
            await _chargingPortHttpService.DeleteAsync(chargingPort.Id);
            
            ChargingPorts = new ObservableCollection<ChargingPort>();
           
        }

        [RelayCommand]
        public async Task DoUpdateStation(ChargingStation chargingStation)
        {
            var editWindow = new EditChargingStationWindowView
            {
                DataContext = new EditChargingStationWindowViewModel(chargingStation, _chargingStationHttpService)
            };
            editWindow.ShowDialog();
            await _chargingStationHttpService.UpdateAsync(chargingStation);
            await UpdateAsync();
        }

        [RelayCommand]
        public async Task DoUpdatePort(ChargingPort chargingPort)
        {
            var viewModel = new EditChargingPortWindowViewModel(
                chargingPort,
                _chargingPortHttpService
            );
            await viewModel.InitializeAsync();

            var editWindow = new EditChargingPortWindowView
            {
                DataContext = viewModel
            };

            editWindow.ShowDialog();
            await _chargingPortHttpService.UpdateAsync(chargingPort);
            await UpdateAsync();
        }

        public override async Task InitializeAsync()
        {
            await UpdateAsync();
            await base.InitializeAsync();
        }

        private async Task UpdateAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ChargingStations = new ObservableCollection<ChargingStation>(_chargingStationAssembler.ToModels(await _chargingStationHttpService.GetFilteredChargingStationsAsync(null)));
            }
            else
            {
                var filteredStations = await _chargingStationHttpService.GetFilteredChargingStationsAsync(SearchText);
                ChargingStations = new ObservableCollection<ChargingStation>(_chargingStationAssembler.ToModels(filteredStations));
            }
            /*List<ChargingStation> chargingStations = await _chargingStationHttpService.GetAllAsync();
            if(chargingStations.Count > 0)
            {
                ChargingStations = new ObservableCollection<ChargingStation>(chargingStations);
            }
            List<ChargingPort> chargingPorts = await _chargingPortHttpService.GetAllAsync();
            if (chargingPorts.Count > 0)
            {
                ChargingPorts = new ObservableCollection<ChargingPort>(chargingPorts);
            }*/

            /*if (SelectedChargingStation != null)
            {
                var filteredStations = await _chargingPortHttpService.GetChargingPortsOfStationsAsync(SelectedChargingStation.Id);
                ChargingPorts = new ObservableCollection<ChargingPort>(_chargingPortAssembler.ToModels(filteredStations));
            }
            else
            {
                ChargingPorts = new ObservableCollection<ChargingPort>();
            }*/
        }
    }
}
