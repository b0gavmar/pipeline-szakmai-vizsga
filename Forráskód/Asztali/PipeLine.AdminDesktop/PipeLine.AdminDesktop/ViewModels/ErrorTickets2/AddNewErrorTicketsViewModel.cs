using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ErrorTicketModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.ErrorTickets2
{
    public partial class AddNewErrorTicketsViewModel : BaseViewModel
    {
        private readonly IErrorTicketHttpService _errorTicketHttpService;
        private readonly IErrorTicketAssembler _errorTicketAssembler;
        private readonly IChargingStationHttpService _chargingStationHttpService;
        private readonly IChargingStationAssembler _stationAssembler;

        public AddNewErrorTicketsViewModel()
        {
            _errorTicketHttpService = new ErrorTicketHttpService();
            _errorTicketAssembler = new ErrorTicketAssembler();
            _chargingStationHttpService = new ChargingStationHttpService();
            _stationAssembler = new ChargingStationAssembler();
        }

        public AddNewErrorTicketsViewModel(IErrorTicketHttpService? errorTicketHttpService, IErrorTicketAssembler? errorTicketAssembler, IChargingStationHttpService chargingStationHttpService, IChargingStationAssembler stationAssembler)
        {
            _errorTicketHttpService = errorTicketHttpService ?? throw new ArgumentNullException(nameof(errorTicketHttpService));
            _errorTicketAssembler = errorTicketAssembler ?? throw new ArgumentNullException(nameof(errorTicketAssembler));
            _chargingStationHttpService = chargingStationHttpService ?? throw new ArgumentNullException(nameof(chargingStationHttpService));
            _stationAssembler = stationAssembler ?? throw new ArgumentNullException(nameof(stationAssembler));
        }

        [ObservableProperty]
        private string _warningMsg = string.Empty;

        [ObservableProperty]
        private ErrorTicket _newErrorTicket = new();

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private ObservableCollection<ChargingStation> _chargingStationList = new ObservableCollection<ChargingStation>();

        [ObservableProperty]
        private ChargingStation _selectedChargingStation = new ChargingStation();

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
            NewErrorTicket.ChargingStationId = value?.Id ?? Guid.Empty; 
        }

        [RelayCommand]
        public async Task AddNewErrorTicket()
        {
            if (NewErrorTicket.ChargingStationId != new Guid() && !string.IsNullOrEmpty(NewErrorTicket.Description))
            {
                var resp = await _errorTicketHttpService.CreateAsync(NewErrorTicket);
                if(resp.IsSuccess)
                {
                    WarningMsg = "Error ticket created successfully";
                    NewErrorTicket = new ErrorTicket();
                }
                else
                {
                    WarningMsg = "Failed to create error ticket";
                }
            }
            else
            {
                WarningMsg = "Please fill out every field";
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
