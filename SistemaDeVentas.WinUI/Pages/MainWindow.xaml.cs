using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SistemaDeVentas.WinUI.Pages;
using System;
using Windows.System;

namespace SistemaDeVentas.WinUI
{
    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer _timeTimer;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Configurar título de la ventana
            Title = "Sistema de Ventas - WinUI 3";

            // Inicializar timer para la hora
            InitializeTimer();

            // Suscribirse al evento Closed
            this.Closed += MainWindow_Closed;

            // Navegar a la página inicial
            NavigateToPage(typeof(SalesPage), "Sales");
        }

        public string CurrentTime { get; private set; } = DateTime.Now.ToString("HH:mm:ss");

        private void InitializeTimer()
        {
            _timeTimer = new DispatcherTimer();
            _timeTimer.Interval = TimeSpan.FromSeconds(1);
            _timeTimer.Tick += (s, e) =>
            {
                CurrentTime = DateTime.Now.ToString("HH:mm:ss");
                TimeTextBlock.Text = CurrentTime;
            };
            _timeTimer.Start();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                var tag = selectedItem.Tag?.ToString();
                NavigateToPageByTag(tag);
            }
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is string invokedItem)
            {
                // Manejar navegación por texto si es necesario
            }
            else if (args.InvokedItemContainer is NavigationViewItem item)
            {
                var tag = item.Tag?.ToString();
                NavigateToPageByTag(tag);
            }
        }

        private void NavigateToPageByTag(string? tag)
        {
            Type? pageType = tag switch
            {
                "Sales" => typeof(SalesPage),
                "Inventory" => typeof(InventoryPage),
                "Reports" => typeof(ReportsPage),
                "Customers" => null, // TODO: Implementar
                "Suppliers" => null, // TODO: Implementar
                "Users" => null, // TODO: Implementar
                "Settings" => null, // TODO: Implementar
                _ => null
            };

            if (pageType != null)
            {
                NavigateToPage(pageType, tag);
            }
            else
            {
                // Mostrar mensaje de página no implementada
                ShowNotImplementedMessage(tag);
            }
        }

        private void NavigateToPage(Type pageType, string? tag)
        {
            try
            {
                ContentFrame.Navigate(pageType);
                
                // Actualizar título de la página
                UpdatePageTitle(tag);
                
                // Actualizar estado
                UpdateStatus($"Navegado a {GetPageDisplayName(tag)}");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error al navegar: {ex.Message}");
            }
        }

        private void UpdatePageTitle(string? tag)
        {
            var displayName = GetPageDisplayName(tag);
            PageTitleTextBlock.Text = $"Sistema de Ventas - {displayName}";
        }

        private string GetPageDisplayName(string? tag)
        {
            return tag switch
            {
                "Sales" => "Punto de Venta",
                "Inventory" => "Inventario",
                "Reports" => "Reportes",
                "Customers" => "Clientes",
                "Suppliers" => "Proveedores",
                "Users" => "Usuarios",
                "Settings" => "Configuración",
                _ => "Sistema de Ventas"
            };
        }

        private async void ShowNotImplementedMessage(string? tag)
        {
            var dialog = new ContentDialog
            {
                Title = "Función no implementada",
                Content = $"La página '{GetPageDisplayName(tag)}' aún no está implementada.",
                CloseButtonText = "Entendido",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private void UpdateStatus(string message)
        {
            StatusTextBlock.Text = message;
            
            // Limpiar el mensaje después de 3 segundos
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (s, e) =>
            {
                StatusTextBlock.Text = "Listo";
                timer.Stop();
            };
            timer.Start();
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Cerrar Sesión",
                Content = "¿Está seguro que desea cerrar la sesión?",
                PrimaryButtonText = "Sí, cerrar sesión",
                CloseButtonText = "Cancelar",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                // TODO: Implementar lógica de cierre de sesión
                // Por ahora, cerrar la aplicación
                Application.Current.Exit();
            }
        }

        private void MainWindow_Closed(object sender, WindowEventArgs e)
        {
            _timeTimer?.Stop();
        }
    }
}