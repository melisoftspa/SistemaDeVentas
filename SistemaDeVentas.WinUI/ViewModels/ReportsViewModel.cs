using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SistemaDeVentas.WinUI.Models;
using SistemaDeVentas.Core.Application.Interfaces;

namespace SistemaDeVentas.WinUI.ViewModels
{
    public class ReportsViewModel : BaseViewModel
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        public ReportsViewModel(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));

            Title = "Reportes";
            
            GenerateSalesReportCommand = new RelayCommand(async () => await GenerateSalesReportAsync());
            GenerateInventoryReportCommand = new RelayCommand(async () => await GenerateInventoryReportAsync());
            ExportReportCommand = new RelayCommand(async () => await ExportReportAsync());
            RefreshCommand = new RelayCommand(async () => await GenerateSalesReportAsync());
            ViewAllSalesCommand = new RelayCommand(async () => await Task.CompletedTask);
            
            // Inicializar fechas por defecto
            StartDate = DateTimeOffset.Now.AddDays(-30);
            EndDate = DateTimeOffset.Now;
        }

        // Propiedades de filtros
        private DateTimeOffset? _startDate;
        public DateTimeOffset? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTimeOffset? _endDate;
        public DateTimeOffset? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private string _selectedReportType = "Ventas";
        public string SelectedReportType
        {
            get => _selectedReportType;
            set => SetProperty(ref _selectedReportType, value);
        }

        private string _selectedPeriod = "Personalizado";
        public string SelectedPeriod
        {
            get => _selectedPeriod;
            set => SetProperty(ref _selectedPeriod, value);
        }

        // Propiedades para tarjetas de resumen
        public string TotalSales => "$0.00";
        public string SalesGrowth => "+0%";
        public string TotalTransactions => "0";
        public string TransactionsGrowth => "+0%";
        public string AverageTicket => "$0.00";
        public string TicketGrowth => "+0%";
        public string ProductsSold => "0";
        public string ProductsGrowth => "+0%";

        // Datos del reporte
        private ObservableCollection<ReportData> _reportData = new();
        public ObservableCollection<ReportData> ReportData
        {
            get => _reportData;
            set => SetProperty(ref _reportData, value);
        }

        public ObservableCollection<object> TopProducts => new();
        public ObservableCollection<object> RecentSales => new();

        // Comandos
        public ICommand GenerateSalesReportCommand { get; }
        public ICommand GenerateInventoryReportCommand { get; }
        public ICommand ExportReportCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ViewAllSalesCommand { get; }
        // Métodos de generación de reportes
        private async Task GenerateSalesReportAsync()
        {
            try
            {
                IsBusy = true;
                ClearError();

                // TODO: Implementar generación de reporte de ventas usando _saleService
                var sales = await _saleService.GetSalesByDateRangeAsync(StartDate?.DateTime ?? DateTime.Now.AddDays(-30), EndDate?.DateTime ?? DateTime.Now);
                
                ReportData.Clear();
                foreach (var sale in sales)
                {
                    ReportData.Add(new ReportData
                    {
                        Date = sale.Date,
                        Description = $"Venta #{sale.Id}",
                        Amount = (decimal)sale.Total,
                        Type = "Venta"
                    });
                }
            }
            catch (Exception ex)
            {
                SetError($"Error al generar reporte de ventas: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GenerateInventoryReportAsync()
        {
            try
            {
                IsBusy = true;
                ClearError();

                // TODO: Implementar generación de reporte de inventario usando _productService
                var products = await _productService.GetAllProductsAsync();
                
                ReportData.Clear();
                foreach (var product in products)
                {
                    ReportData.Add(new ReportData
                    {
                        Date = DateTime.Now,
                        Description = product.Name,
                        Amount = (decimal)product.Price,
                        Type = "Producto"
                    });
                }
            }
            catch (Exception ex)
            {
                SetError($"Error al generar reporte de inventario: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExportReportAsync()
        {
            try
            {
                IsBusy = true;
                ClearError();

                // TODO: Implementar exportación de reporte (PDF, Excel, etc.)
                await Task.Delay(1000); // Simular exportación
                
                // Aquí se implementaría la lógica de exportación
            }
            catch (Exception ex)
            {
                SetError($"Error al exportar reporte: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    // Clase auxiliar para datos de reporte
    public class ReportData
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}