using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using SistemaDeVentas.Core.ViewModels.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SistemaDeVentas.WinUI.Pages
{
    public sealed partial class SalesPage : Page
    {
        public SalesViewModel ViewModel { get; }
        public PrintViewModel PrintViewModel { get; }

        public SalesPage()
        {
            this.InitializeComponent();
            ViewModel = ((App)Application.Current).Services.GetRequiredService<SalesViewModel>();
            PrintViewModel = ((App)Application.Current).Services.GetRequiredService<PrintViewModel>();

            // Suscribirse a cambios en LastProcessedSaleId para actualizar PrintViewModel
            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Start page load animation
            var storyboard = (Storyboard)Resources["PageLoadStoryboard"];
            storyboard?.Begin();
        }

        private async void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.LastProcessedSaleId) && ViewModel.LastProcessedSaleId.HasValue)
            {
                // Venta completada, actualizar PrintViewModel con los datos de la venta
                await UpdatePrintViewModelWithSaleData(ViewModel.LastProcessedSaleId.Value);
            }
        }

        private async Task UpdatePrintViewModelWithSaleData(Guid saleId)
        {
            try
            {
                // Obtener la venta procesada
                var sale = await ViewModel.GetProcessedSaleAsync();
                PrintViewModel.CurrentSale = sale;

                // Si es una factura (DTE 33 o 34), obtener el DteDocument
                if (ViewModel.SelectedDteType == 33 || ViewModel.SelectedDteType == 34)
                {
                    // TODO: Obtener DteDocument desde el servicio DTE
                    // PrintViewModel.CurrentDteDocument = await GetDteDocumentForSale(saleId);
                }
            }
            catch (Exception ex)
            {
                // Manejar error - PrintViewModel no tiene SetError público, usar logging o algo
                // PrintViewModel.SetError($"Error al preparar datos de impresión: {ex.Message}");
            }
        }
    }
}