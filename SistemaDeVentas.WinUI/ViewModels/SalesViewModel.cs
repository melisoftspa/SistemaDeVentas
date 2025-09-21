using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SistemaDeVentas.WinUI.Models;
using SistemaDeVentas.Core.Application.Interfaces;

namespace SistemaDeVentas.WinUI.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public SalesViewModel(ISaleService saleService, IProductService productService, IUserService userService)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            Title = "Punto de Venta";
            CartItems = new ObservableCollection<Detail>();
            
            AddProductCommand = new RelayCommand(async () => await AddProductAsync());
            RemoveItemCommand = new RelayCommand<Detail>(async (item) => await RemoveItemAsync(item));
            ProcessSaleCommand = new RelayCommand(async () => await ProcessSaleAsync(), CanProcessSale);
            ClearCartCommand = new RelayCommand(async () => await ClearCartAsync());
            
            // Actualizar totales cuando cambie el carrito
            CartItems.CollectionChanged += (s, e) => UpdateTotals();
        }

        private ObservableCollection<Detail> _cartItems = new();
        public ObservableCollection<Detail> CartItems
        {
            get => _cartItems;
            set => SetProperty(ref _cartItems, value);
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private string _selectedPaymentMethod = string.Empty;
        public string SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set
            {
                if (SetProperty(ref _selectedPaymentMethod, value))
                {
                    ((RelayCommand)ProcessSaleCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private double _subtotal;
        public double Subtotal
        {
            get => _subtotal;
            private set => SetProperty(ref _subtotal, value);
        }

        private double _tax;
        public double Tax
        {
            get => _tax;
            private set => SetProperty(ref _tax, value);
        }

        private double _total;
        public double Total
        {
            get => _total;
            private set => SetProperty(ref _total, value);
        }

        public string CurrentDateTime => DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm");

        // Propiedades formateadas para la UI
        public string FormattedSubtotal => Subtotal.ToString("C");
        public string FormattedTax => Tax.ToString("C");
        public string FormattedTotal => Total.ToString("C");

        // Comandos
        public ICommand AddProductCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ProcessSaleCommand { get; }
        public ICommand ClearCartCommand { get; }

        private async Task AddProductAsync()
        {
            try
            {
                IsBusy = true;
                ClearError();

                // Buscar producto por código de barras o nombre
                var products = await _productService.GetAllProductsAsync();
                var product = products.FirstOrDefault(p =>
                    p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    (p.Barcode != null && p.Barcode.Equals(SearchText, StringComparison.OrdinalIgnoreCase)));

                if (product == null)
                {
                    SetError("Producto no encontrado");
                    return;
                }

                if (!product.IsActive)
                {
                    SetError("El producto no está activo");
                    return;
                }

                if (product.Stock <= 0)
                {
                    SetError("Producto sin stock disponible");
                    return;
                }

                // Verificar si el producto ya está en el carrito
                var existingItem = CartItems.FirstOrDefault(item => item.ProductName == product.Name);
                if (existingItem != null)
                {
                    if (existingItem.Amount >= product.Stock)
                    {
                        SetError("No hay suficiente stock para agregar más unidades");
                        return;
                    }
                    existingItem.Amount++;
                }
                else
                {
                    var newItem = new Detail
                    {
                        ProductName = product.Name,
                        Amount = 1,
                        Price = product.SalePrice,
                        Tax = product.Exenta ? 0 : 19 // Asumir IVA 19% si no está exento
                    };
                    
                    CartItems.Add(newItem);
                }

                UpdateTotals();
                SearchText = string.Empty; // Limpiar búsqueda
            }
            catch (Exception ex)
            {
                SetError($"Error al agregar producto: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RemoveItemAsync(Detail? item)
        {
            if (item != null && CartItems.Contains(item))
            {
                CartItems.Remove(item);
                UpdateTotals();
            }
        }

        private bool CanProcessSale()
        {
            return CartItems.Count > 0 && 
                   !string.IsNullOrEmpty(SelectedPaymentMethod) && 
                   !IsBusy;
        }

        private async Task ProcessSaleAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ClearError();

                // Crear la venta usando el servicio
                var saleData = new
                {
                    Date = DateTime.Now,
                    Total = Total,
                    Tax = Tax,
                    PaymentMethod = SelectedPaymentMethod,
                    Items = CartItems.Select(item => new
                    {
                        ProductName = item.ProductName,
                        Amount = item.Amount,
                        Price = item.Price,
                        Subtotal = item.Subtotal,
                        Tax = item.TaxAmount,
                        Total = item.Total
                    }).ToList()
                };

                // TODO: Implementar creación de venta cuando el servicio esté disponible
                // await _saleService.CreateSaleAsync(saleData);

                // Simular procesamiento por ahora
                await Task.Delay(1000);

                // Limpiar carrito después de venta exitosa
                await ClearCartAsync();

                // Mostrar mensaje de éxito
                // TODO: Implementar notificación de éxito
            }
            catch (Exception ex)
            {
                SetError($"Error al procesar la venta: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ClearCartAsync()
        {
            CartItems.Clear();
            UpdateTotals();
            SelectedPaymentMethod = string.Empty;
        }

        private void UpdateTotals()
        {
            Subtotal = CartItems.Sum(item => item.Subtotal);
            Tax = CartItems.Sum(item => item.TaxAmount);
            Total = Subtotal + Tax;

            // Notificar cambios en propiedades formateadas
            OnPropertyChanged(nameof(FormattedSubtotal));
            OnPropertyChanged(nameof(FormattedTax));
            OnPropertyChanged(nameof(FormattedTotal));
            
            ((RelayCommand)ProcessSaleCommand).RaiseCanExecuteChanged();
        }
    }
}