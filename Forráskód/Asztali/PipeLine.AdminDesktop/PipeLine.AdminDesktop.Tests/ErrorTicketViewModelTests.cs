using Moq;
using PipeLine.AdminDesktop.ViewModels.ErrorTickets2;
using PipeLine.Desktop.ViewModels;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PipeLine.AdminDesktop.Tests
{
    public class ErrorTicketViewModelTests
    {
        [Fact]
        public async Task ShowErrorTicketsListView_ShouldUpdateView()
        {
            var mockListViewModel = new Mock<ErrorTicketsListViewModel2>();
            var mockAddNewViewModel = new Mock<AddNewErrorTicketsViewModel>();

            var viewModel = new ErrorTicketsViewModel2(mockListViewModel.Object, mockAddNewViewModel.Object);

            await viewModel.ShowErrorTicketsListView();

            Assert.IsAssignableFrom<ErrorTicketsListViewModel2>(viewModel.CurrentErrorTicketChildView);
            mockListViewModel.Verify(vm => vm.InitializeAsync(), Times.Once);
        }
    }
}
