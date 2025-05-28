using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace PipeLine.AdminDesktop.ViewModels.Users
{
    public partial class UsersViewModel : BaseViewModel
    {
        public ApplicationUserViewModel _applicationUserViewModel;
        public UnifiedUserViewModel _unifiedApplicationUserViewModel;
        public AddNewUserViewModel _addNewApplicationUserViewModel;

        public UsersViewModel()
        {
            _applicationUserViewModel = new ApplicationUserViewModel();
            _currentUserChildView = new UnifiedUserViewModel();
            _unifiedApplicationUserViewModel = new UnifiedUserViewModel();
            _addNewApplicationUserViewModel = new AddNewUserViewModel();
        }

        public UsersViewModel(ApplicationUserViewModel applicationUserViewModel, UnifiedUserViewModel unifiedApplicationUserViewModel, AddNewUserViewModel addNewApplicationUserViewModel)
        {
            _applicationUserViewModel = applicationUserViewModel;
            _currentUserChildView = unifiedApplicationUserViewModel;
            _unifiedApplicationUserViewModel = unifiedApplicationUserViewModel;
            _addNewApplicationUserViewModel = addNewApplicationUserViewModel;
        }

        [ObservableProperty]
        private BaseViewModel _currentUserChildView;

        [RelayCommand]
        public void ShowApplicationUserView()
        {
            CurrentUserChildView = _applicationUserViewModel;
        }

        [RelayCommand]
        public async Task ShowUnifiedApplicationUserView()
        {
            await _unifiedApplicationUserViewModel.InitializeAsync();
            CurrentUserChildView = _unifiedApplicationUserViewModel;
        }

        [RelayCommand]
        public async Task ShowAddNewApplicationUserView()
        {
            await _addNewApplicationUserViewModel.InitializeAsync();
            CurrentUserChildView = _addNewApplicationUserViewModel;
        }
    }
}