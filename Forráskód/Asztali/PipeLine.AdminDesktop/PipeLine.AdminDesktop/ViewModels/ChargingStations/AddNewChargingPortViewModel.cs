using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers.ChargingPortAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ChargingStations
{
    public partial class AddNewChargingPortViewModel : BaseViewModel
    {
        private readonly IChargingPortHttpService _chargingPortHttpService;
        private readonly IChargingPortAssembler _chargingPortAssembler;
        private readonly IChargingStationHttpService _chargingStationHttpService;
        private readonly IChargingStationAssembler _stationAssembler;

        public AddNewChargingPortViewModel()
        {
            _chargingPortHttpService = new ChargingPortHttpService();
            _chargingPortAssembler = new ChargingPortAssembler();
            _chargingStationHttpService = new ChargingStationHttpService();
            _stationAssembler = new ChargingStationAssembler();
        }

        public AddNewChargingPortViewModel(IChargingPortHttpService? chargingPortHttpService, IChargingPortAssembler? chargingPortAssembler, IChargingStationHttpService? chargingStationHttpService, IChargingStationAssembler? chargingStationAssembler)
        {
            _chargingPortHttpService = chargingPortHttpService ?? throw new ArgumentNullException(nameof(chargingPortHttpService));
            _chargingPortAssembler = chargingPortAssembler ?? throw new ArgumentNullException(nameof(chargingPortAssembler));
            _chargingStationHttpService = chargingStationHttpService ?? throw new ArgumentNullException(nameof(chargingStationHttpService));
            _stationAssembler = chargingStationAssembler ?? throw new ArgumentNullException(nameof(chargingStationAssembler));
        }

        [ObservableProperty]
        private ChargingPort _newChargingPort = new();

        [ObservableProperty]
        private string _warningMsg = string.Empty;

        [ObservableProperty]
        private ObservableCollection<ChargingStation> _chargingStationList = new ObservableCollection<ChargingStation>();

        [ObservableProperty]
        private ChargingStation _selectedChargingStation = new ChargingStation();

        [ObservableProperty]
        private string _searchText;

        partial void OnSearchTextChanged(string value)
        {
            _ = FilterChargingStationsAsync(value);
        }

        private async Task FilterChargingStationsAsync(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                ChargingStationList = new ObservableCollection<ChargingStation>(await _chargingStationHttpService.GetAllAsync());
            }
            else
            {
                var filteredStations = await _chargingStationHttpService.GetFilteredChargingStationsAsync(searchInput);
                ChargingStationList = new ObservableCollection<ChargingStation>((IEnumerable<ChargingStation>)filteredStations);
            }
        }


        partial void OnSelectedChargingStationChanged(ChargingStation value)
        {
            NewChargingPort.ChargingStationId = value?.Id ?? Guid.Empty;
        }

        [RelayCommand]
        public async Task AddNewChargingPort()
        {
            if(NewChargingPort.PortNumber > 0 && NewChargingPort.ChargingStationId != new Guid())
            {
                await _chargingPortHttpService.CreateAsync(NewChargingPort);
                WarningMsg = "Created successfully";

            }
            else
            {
                WarningMsg = "Fill out every field";
            }

        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
            ChargingStationList = new ObservableCollection<ChargingStation>((IEnumerable<ChargingStation>)_stationAssembler.ToModels(await _chargingStationHttpService.GetFilteredChargingStationsAsync(null)));
            SelectedChargingStation = ChargingStationList[0];
        }
    }
}
