using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.Views;
using PipeLine.Desktop.Views;
using PipeLine.HttpService.Dtos;
using PipeLine.HttpService.Services;
using PipeLine.Shared.Dtos.UserDtos;

namespace PipeLine.Desktop.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private MainView _mainView;
        private string _email;
        private string _password;
        private bool _isLoading;

        private readonly LoginHttpService _loginHttpService;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {

        }

        public LoginViewModel(LoginHttpService loginHttpService, MainView mainView)
        {
            _loginHttpService = loginHttpService;
            LoginCommand = new RelayCommand(async () => await Login(), () => !IsLoading);
            _mainView = mainView;
        }

        private async Task Login()
        {
            IsLoading = true;
            try
            {
                var loginDto = new LoginRegisterRequest(Email, Password);
                var success = await _loginHttpService.LoginAsync(loginDto);
                if (!string.IsNullOrEmpty(success))
                {
                    MessageBox.Show("Login Successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    _mainView.Show();

                    Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w is LoginView)?.Close();
                }
                else
                {
                    MessageBox.Show("Invalid credentials!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Password = string.Empty;
                }
            }
            finally
            {
                IsLoading = false;
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
