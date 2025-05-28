using PipeLine.AdminDesktop.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using PipeLine.HttpService;
using PipeLine.Backend.Assemblers.UserAssemblers;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Dtos.UserDtos;
using Mysqlx.Crud;
using Microsoft.AspNetCore.Identity;
using PipeLine.HttpService.Services;
using System.Windows;
using Mysqlx.Notice;



namespace PipeLine.AdminDesktop.ViewModels.Users
{
    public partial class AddNewUserViewModel : BaseViewModel
    {
        private readonly IApplicationUserHttpService _applicationUserHttpService;
        private readonly LoginHttpService _loginHttpService;
        private readonly IApplicationUserAssembler _assembler;

        public AddNewUserViewModel()
        {
            _applicationUserHttpService = new ApplicationUserHttpService();
            _assembler = new ApplicationUserAssembler();
        }

        public AddNewUserViewModel(IApplicationUserHttpService? applicationUserHttpService, LoginHttpService loginHttpService, IApplicationUserAssembler assembler)
        {
            _applicationUserHttpService = applicationUserHttpService ?? throw new ArgumentNullException(nameof(applicationUserHttpService));
            _loginHttpService = loginHttpService ?? throw new ArgumentNullException(nameof(loginHttpService));
            _assembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
        }

        [ObservableProperty]
        private ApplicationUser _newApplicationUser = new();

        [ObservableProperty]
        private string _password = string.Empty;


        [ObservableProperty]
        private string _warningMsg = string.Empty;

        [RelayCommand]
        public async Task AddNewUser()
        {

            if (!string.IsNullOrEmpty(NewApplicationUser.Email) && !string.IsNullOrEmpty(Password))
            {

                    var request = new LoginRegisterRequest(NewApplicationUser.Email, Password);
                    var resp = await _loginHttpService.RegisterAsync(request);

                    if(string.IsNullOrEmpty(resp))
                    {
                        WarningMsg = "The password or email is invalid";
                    }
                    else
                    {
                        WarningMsg = "Administrator added successfully";
                    }


                    
                    //ApplicationUserDto user = await _applicationUserHttpService.GetUserByEmailAsync(NewApplicationUser.Email);
                    //await _applicationUserHttpService.UpdateAsync(_assembler.ToModel(user));

                
            }
            else
            {
                WarningMsg = "The email and password cannot be null or empty";
            }
        }

        public async override Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
