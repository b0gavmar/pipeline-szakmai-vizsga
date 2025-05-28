using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.Views.Users;
using PipeLine.HttpService;
using PipeLine.Shared.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.Users
{
    public partial class UnifiedUserViewModel : BaseViewModel
    {
        private readonly IApplicationUserHttpService _applicationUserHttpService;

        [ObservableProperty]
        private ObservableCollection<ApplicationUser> _applicationUsers = new();

        [ObservableProperty]
        private ApplicationUser _selectedApplicationUser = new ApplicationUser();

        public UnifiedUserViewModel()
        {
            _applicationUserHttpService = new ApplicationUserHttpService();
            _selectedApplicationUser = new ApplicationUser();
        }

        public UnifiedUserViewModel(IApplicationUserHttpService? applicationUserHttpService)
        {
            _applicationUserHttpService = applicationUserHttpService ?? throw new ArgumentNullException(nameof(applicationUserHttpService));
            _selectedApplicationUser = new ApplicationUser();
        }

        [RelayCommand]
        public async Task DoRemove(ApplicationUser applicationUser)
        {
            await _applicationUserHttpService.DeleteAsync(applicationUser.Id);
            await UpdateAsync();
        }

        [RelayCommand]
        public async Task DoUpdate(ApplicationUser applicationUser)
        {
            var editWindow = new EditUserWindowView
            {
                DataContext = new EditUserWindowViewModel(applicationUser)
            };
            editWindow.ShowDialog();

            await _applicationUserHttpService.UpdateAsync(applicationUser);
            await UpdateAsync();
        }

        public override async Task InitializeAsync()
        {
            await UpdateAsync();
            await base.InitializeAsync();
        }

        private async Task UpdateAsync()
        {
            List<ApplicationUser> applicationUsers = await _applicationUserHttpService.GetAllAsync();
            if(applicationUsers.Count > 0)
            {
                ApplicationUsers = new ObservableCollection<ApplicationUser>(applicationUsers);
            }
        }
    }
}
