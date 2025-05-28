using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.ApplicationServices;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.HttpService;
using PipeLine.Shared.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PipeLine.AdminDesktop.ViewModels.Users
{
    public partial class EditUserWindowViewModel : BaseViewModel
    {
        private readonly IApplicationUserHttpService _applicationUserHttpService = new ApplicationUserHttpService();

        [ObservableProperty]
        private ApplicationUser _applicationUser;

        public EditUserWindowViewModel(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        [RelayCommand]
        public async Task DoUpdate(Window window)
        {
            if (!string.IsNullOrEmpty(ApplicationUser.Email))
            {
                await _applicationUserHttpService.UpdateAsync(ApplicationUser);
                window.Close();
            }
        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
