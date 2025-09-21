using Microsoft.UI.Xaml.Controls;
using SistemaDeVentas.Core.ViewModels.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace SistemaDeVentas.WinUI.Pages
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel ViewModel { get; }
        public UserManagementViewModel UserManagementViewModel { get; }

        public SettingsPage()
        {
            this.InitializeComponent();

            // Get the ViewModels from DI container
            ViewModel = ((App)Microsoft.UI.Xaml.Application.Current).Services.GetService<SettingsViewModel>()!;
            UserManagementViewModel = ((App)Microsoft.UI.Xaml.Application.Current).Services.GetService<UserManagementViewModel>()!;

            // Load settings when page is loaded
            Loaded += (s, e) => ViewModel.LoadSettingsCommand.Execute(null);
        }
    }
}