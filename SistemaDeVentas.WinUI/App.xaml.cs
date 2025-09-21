using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SistemaDeVentas.Infrastructure.DependencyInjection;
using SistemaDeVentas.WinUI.ViewModels;
using SistemaDeVentas.WinUI.Services;
using System;

namespace SistemaDeVentas.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;
        public IServiceProvider Services { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            
            // Register infrastructure services (repositories and application services)
            services.AddInfrastructureServices("Data Source=SalesSystem.db");
            
            // Register WinUI specific services
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            
            // Register ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<SalesViewModel>();
            services.AddTransient<InventoryViewModel>();
            services.AddTransient<ReportsViewModel>();

            Services = services.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();
            _window.Activate();
        }
    }
}
