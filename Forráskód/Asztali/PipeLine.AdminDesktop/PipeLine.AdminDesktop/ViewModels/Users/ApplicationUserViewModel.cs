using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.AdminDesktop.Repos;
using PipeLine.Shared.Models.UserModels;
using System.Collections.ObjectModel;

namespace PipeLine.AdminDesktop.ViewModels.Users
{
    public partial class ApplicationUserViewModel : BaseViewModel
    {
        private ApplicationUserRepo _applicationUserRepo = new();


        [ObservableProperty]
        private ObservableCollection<ApplicationUser> _applicationUsers = new();

        [ObservableProperty]
        private ApplicationUser _selectedApplicationUser = new();

        public ApplicationUserViewModel()
        {
            _selectedApplicationUser = new ApplicationUser();
            UpdateView();
        }


        [RelayCommand]
        void DoNewApplicationUser()
        {
            SelectedApplicationUser = new ApplicationUser();
        }

        [RelayCommand]
        public void DoRemove(ApplicationUser applicationUserToDelete)
        {
            _applicationUserRepo.Delete(applicationUserToDelete);
            UpdateView();
        }

        [RelayCommand]
        public void DoSave(ApplicationUser applicationUserToSave)
        {
            _applicationUserRepo.Save(applicationUserToSave);
            UpdateView();
        }
        private void UpdateView()
        {
            ApplicationUsers = new ObservableCollection<ApplicationUser>(_applicationUserRepo.FindAll());
        }

    }
}
