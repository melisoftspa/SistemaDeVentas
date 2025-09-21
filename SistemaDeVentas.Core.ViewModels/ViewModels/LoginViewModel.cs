using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoreInterfaces = SistemaDeVentas.Core.Application.Interfaces;

namespace SistemaDeVentas.Core.ViewModels.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly CoreInterfaces.IAuthenticationService _authService;
        private readonly CoreInterfaces.INavigationService _navigationService;

        public LoginViewModel(CoreInterfaces.IAuthenticationService authService, CoreInterfaces.INavigationService navigationService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            Title = "Iniciar Sesión";
            LoginCommand = new RelayCommand(async () => await LoginAsync(), CanLogin);
        }

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                {
                    ClearError();
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    ClearError();
                    ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get => _rememberMe;
            set => SetProperty(ref _rememberMe, value);
        }

        public ICommand LoginCommand { get; }

        private bool CanLogin()
        {
            return !IsBusy && 
                   !string.IsNullOrWhiteSpace(Username) && 
                   !string.IsNullOrWhiteSpace(Password);
        }

        private async Task LoginAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ClearError();

                var result = await _authService.LoginAsync(Username, Password);

                if (result.IsSuccess)
                {
                    // Navegar a la página principal
                    _navigationService.NavigateToMain();
                }
                else
                {
                    SetError(result.ErrorMessage ?? "Error de autenticación");
                }
            }
            catch (Exception ex)
            {
                SetError($"Error inesperado: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}