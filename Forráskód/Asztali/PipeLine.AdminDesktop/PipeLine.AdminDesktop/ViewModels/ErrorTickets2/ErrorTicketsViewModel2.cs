using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
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
    public partial class ErrorTicketsViewModel2: BaseViewModel
    {
        public ErrorTicketsListViewModel2 _listViewModel2;
        public AddNewErrorTicketsViewModel _addNewErrorTicketsViewModel;

        public ErrorTicketsViewModel2()
        {
            _listViewModel2 = new ErrorTicketsListViewModel2();
            _addNewErrorTicketsViewModel = new AddNewErrorTicketsViewModel();
            _currentErrorTicketChildView = _listViewModel2;
        }

        public ErrorTicketsViewModel2(ErrorTicketsListViewModel2 listViewModel2, AddNewErrorTicketsViewModel addNewErrorTicketsViewModel)
        {
            _listViewModel2 = listViewModel2;
            _addNewErrorTicketsViewModel = addNewErrorTicketsViewModel;
            _currentErrorTicketChildView = listViewModel2;
        }

        [ObservableProperty]
        private BaseViewModel _currentErrorTicketChildView;

        [RelayCommand]
        public async Task ShowErrorTicketsListView()
        {
            await _listViewModel2.InitializeAsync();
            CurrentErrorTicketChildView = _listViewModel2;
        }

        [RelayCommand]
        public async Task ShowAddNewErrorTicketsView()
        {
            await _addNewErrorTicketsViewModel.InitializeAsync();
            CurrentErrorTicketChildView = _addNewErrorTicketsViewModel;
        }

    }
}
