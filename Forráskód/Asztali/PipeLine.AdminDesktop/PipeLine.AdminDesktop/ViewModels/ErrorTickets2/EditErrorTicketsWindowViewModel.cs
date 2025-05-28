using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.Backend.Assemblers.ChargingStationAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipeLine.AdminDesktop.ViewModels.ErrorTickets2
{
    public partial class EditErrorTicketsWindowViewModel : BaseViewModel
    {
        private readonly IErrorTicketHttpService _errorTicketHttpService = new ErrorTicketHttpService();
        private readonly IChargingStationHttpService _chargingStationHttpService = new ChargingStationHttpService();
        private readonly IChargingStationAssembler _stationAssembler = new ChargingStationAssembler();

        [ObservableProperty]
        private ErrorTicket _errorTicket;

       

        public EditErrorTicketsWindowViewModel(ErrorTicket errorTicket, IChargingStationHttpService? chargingStationHttpService, IErrorTicketHttpService? errorTicketHttpService, IChargingStationAssembler? chargingStationAssembler)
        {
            ErrorTicket = errorTicket;
            _chargingStationHttpService = chargingStationHttpService ?? throw new ArgumentNullException(nameof(chargingStationHttpService));
            _errorTicketHttpService = errorTicketHttpService ?? throw new ArgumentNullException(nameof(errorTicketHttpService));
            _stationAssembler = chargingStationAssembler ?? throw new ArgumentNullException(nameof(chargingStationAssembler));
        }




        [RelayCommand]
        public async Task DoUpdate(Window window)
        {
            await _errorTicketHttpService.UpdateAsync(ErrorTicket);
            window.Close();
        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
