using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using SistemaDeVentas.WinUI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SistemaDeVentas.WinUI.Pages;

public sealed partial class LoginPage : Page
{
    private readonly IAuthenticationService _authService;

    public LoginPage()
    {
        this.InitializeComponent();
        _authService = ((App)Application.Current).Services.GetRequiredService<IAuthenticationService>();
    }

    private async void SignInButton_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameTextBox.Text;
        var password = PasswordBox.Password;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ErrorTextBlock.Text = "Por favor, ingrese usuario y contraseña";
            ErrorBorder.Visibility = Visibility.Visible;
            return;
        }

        SignInButton.IsEnabled = false;
        LoadingOverlay.Visibility = Visibility.Visible;
        ErrorBorder.Visibility = Visibility.Collapsed;

        try
        {
            var result = await _authService.LoginAsync(username, password);
            if (result.IsSuccess)
            {
                // Navegar a la página principal
                Frame.Navigate(typeof(MainShell));
            }
            else
            {
                ErrorTextBlock.Text = result.ErrorMessage ?? "Error de autenticación";
                ErrorBorder.Visibility = Visibility.Visible;
            }
        }
        catch (Exception ex)
        {
            ErrorTextBlock.Text = $"Error: {ex.Message}";
            ErrorBorder.Visibility = Visibility.Visible;
        }
        finally
        {
            SignInButton.IsEnabled = true;
            LoadingOverlay.Visibility = Visibility.Collapsed;
        }
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        // Start page load animation
        var storyboard = (Storyboard)Resources["PageLoadStoryboard"];
        storyboard.Begin();
    }
}
