using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.Views.ErrorTickets2;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ErrorTickets2
{
    public partial class ErrorTicketsListViewModel2: BaseViewModel
    {
        private readonly IErrorTicketHttpService _errorTicketHttpService;
        private readonly IChargingStationHttpService _chargingStationHttpService;
        private readonly IChargingStationAssembler _stationAssembler;
        private readonly IErrorTicketAssembler _errorTicketAssembler;

        [ObservableProperty]
        private ObservableCollection<ErrorTicket> _errorTickets = new();

        [ObservableProperty]
        private ObservableCollection<ChargingStation> _chargingStations = new();

        [ObservableProperty]
        private ChargingStation _selectedChargingStation;

        [ObservableProperty]
        private ErrorTicket _selectedErrorTicket = new ErrorTicket();

        public ErrorTicketsListViewModel2()
        {
            _chargingStationHttpService = new ChargingStationHttpService();
            _errorTicketHttpService = new ErrorTicketHttpService();
            _selectedErrorTicket = new ErrorTicket();
            _selectedChargingStation = new ChargingStation();
            _stationAssembler = new ChargingStationAssembler();
            _errorTicketAssembler = new ErrorTicketAssembler();
        }

        public ErrorTicketsListViewModel2(IErrorTicketHttpService? errorTicketHttpService, IChargingStationHttpService? chargingStationHttpService, IChargingStationAssembler? chargingStationAssembler, IErrorTicketAssembler errorTicketAssembler)
        {
            _errorTicketHttpService = errorTicketHttpService ?? throw new ArgumentNullException(nameof(errorTicketHttpService));
            _chargingStationHttpService = chargingStationHttpService ?? throw new ArgumentNullException(nameof(chargingStationHttpService));
            _stationAssembler = chargingStationAssembler ?? throw new ArgumentNullException(nameof(chargingStationAssembler));
            _errorTicketAssembler = errorTicketAssembler ?? throw new ArgumentNullException(nameof(errorTicketAssembler));
            _selectedErrorTicket = new ErrorTicket();
            _selectedChargingStation = new ChargingStation();
        }

        [ObservableProperty]
        private string _searchText = string.Empty;

        partial void OnSearchTextChanged(string value)
        {
            _ = FilterChargingStationsAsync(value);
        }

        private async Task FilterChargingStationsAsync(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                ChargingStations = new ObservableCollection<ChargingStation>((IEnumerable<ChargingStation>)_stationAssembler.ToModels(await _chargingStationHttpService.GetFilteredChargingStationsAsync(null)));
            }
            else
            {
                var filteredStations = await _chargingStationHttpService.GetFilteredChargingStationsAsync(searchInput);
                ChargingStations = new ObservableCollection<ChargingStation>((IEnumerable<ChargingStation>)filteredStations);
            }
        }

        [RelayCommand]
        public async Task SearchTicketsOfStation()
        {
            if (string.IsNullOrWhiteSpace(SearchText) || SelectedChargingStation == null)
            {
                ErrorTickets = new ObservableCollection<ErrorTicket>(await _errorTicketHttpService.GetAllAsync());
            }
            else
            {
                var filteredTickets = await _errorTicketHttpService.GetAllTicketsByChargingStationIdAsync(SelectedChargingStation.Id);
                ErrorTickets = new ObservableCollection<ErrorTicket>(_errorTicketAssembler.ToModels(filteredTickets));
            }
            
        }

        [RelayCommand]
        public async Task DoRemove(ErrorTicket errorTicket)
        {
            await _errorTicketHttpService.DeleteAsync(errorTicket.Id);
            await UpdateAsync();
        }

        [RelayCommand]
        public async Task DoUpdate(ErrorTicket errorTicket)
        {
            var viewModel = new EditErrorTicketsWindowViewModel(
                errorTicket,
                _chargingStationHttpService,
                _errorTicketHttpService,
                _stationAssembler
                );
            await viewModel.InitializeAsync();

            var editWindow = new EditErrorTicketsWindowView
            {
                DataContext = viewModel
            };
            editWindow.ShowDialog();
            await _errorTicketHttpService.UpdateAsync(errorTicket);
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
                ChargingStations = new ObservableCollection<ChargingStation>((IEnumerable<ChargingStation>)_stationAssembler.ToModels(await _chargingStationHttpService.GetFilteredChargingStationsAsync(null)));
            }
            else
            {
                var filteredStations = await _chargingStationHttpService.GetFilteredChargingStationsAsync(SearchText);
                ChargingStations = new ObservableCollection<ChargingStation>((IEnumerable<ChargingStation>)_stationAssembler.ToModels(filteredStations));
            }
            
            /*List<ErrorTicket> tickets = await _errorTicketHttpService.GetAllAsync();
            ErrorTickets = new ObservableCollection<ErrorTicket>(tickets);
            */

        }
    }
}
