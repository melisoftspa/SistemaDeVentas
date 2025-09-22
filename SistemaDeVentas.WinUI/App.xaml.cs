using Microsoft.UI.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Infrastructure.DependencyInjection;
using SistemaDeVentas.Core.ViewModels.ViewModels;
using SistemaDeVentas.WinUI.Services;
using CoreInterfaces = SistemaDeVentas.Core.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Infrastructure.Data;

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
            try
            {
                InitializeComponent();

                // Load configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var services = new ServiceCollection();

                // Add logging
                services.AddLogging(configure => configure.AddConsole().AddDebug());

                // Add configuration to services
                services.AddSingleton<IConfiguration>(configuration);

                // Get connection string from configuration
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
                }

                // Validate database connection before registering services
                ValidateDatabaseConnection(connectionString);

                // Register infrastructure services (repositories and application services)
                services.AddInfrastructureServices(connectionString);

                // Register WinUI specific services
                services.AddSingleton<CoreInterfaces.INavigationService, NavigationService>();
                services.AddSingleton<CoreInterfaces.IAuthenticationService, AuthenticationService>();
                services.AddSingleton<CoreInterfaces.IDetailFactory, DetailFactory>();

                // Register ViewModels
                services.AddTransient<LoginViewModel>();
                services.AddTransient<SalesViewModel>();
                services.AddTransient<PrintViewModel>();
                services.AddTransient<InventoryViewModel>();
                services.AddTransient<ReportsViewModel>();
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<UserManagementViewModel>();

                Services = services.BuildServiceProvider();

                // Log successful initialization
                var logger = Services.GetRequiredService<ILogger<App>>();
                logger.LogInformation("Application initialized successfully.");
            }
            catch (Exception ex)
            {
                // Log the error and re-throw to prevent app from starting
                Console.WriteLine($"Critical error during application initialization: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Critical error during application initialization: {ex}");
                throw;
            }
        }

        private void ValidateDatabaseConnection(string connectionString)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<SalesSystemDbContext>()
                    .UseSqlServer(connectionString);

                using var context = new SalesSystemDbContext(optionsBuilder.Options);
                var canConnect = context.Database.CanConnect();
                if (!canConnect)
                {
                    throw new InvalidOperationException("Cannot connect to the database. Please check the connection string and ensure the database server is running.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database connection validation failed: {ex.Message}", ex);
            }
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
