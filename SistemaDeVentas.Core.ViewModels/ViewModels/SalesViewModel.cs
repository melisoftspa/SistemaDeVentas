using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.UI.Xaml.Media.Imaging;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Infrastructure.Services.DTE;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Windows.Storage.Streams;

namespace SistemaDeVentas.Core.ViewModels.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IDteSaleService _dteSaleService;
        private readonly IPdf417Service _pdf417Service;
        private readonly IStampingService _stampingService;
        private readonly IDetailFactory _detailFactory;

        public SalesViewModel(ISaleService saleService, IProductService productService, IUserService userService,
                              IDteSaleService dteSaleService, IPdf417Service pdf417Service, IStampingService stampingService,
                              IDetailFactory detailFactory)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _dteSaleService = dteSaleService ?? throw new ArgumentNullException(nameof(dteSaleService));
            _pdf417Service = pdf417Service ?? throw new ArgumentNullException(nameof(pdf417Service));
            _stampingService = stampingService ?? throw new ArgumentNullException(nameof(stampingService));
            _detailFactory = detailFactory ?? throw new ArgumentNullException(nameof(detailFactory));

            Title = "Punto de Venta";
            CartItems = new ObservableCollection<IDetail>();
            
            AddProductCommand = new RelayCommand(async () => await AddProductAsync());
            RemoveItemCommand = new RelayCommand<IDetail>(async (item) => await RemoveItemAsync(item));
            ProcessSaleCommand = new RelayCommand(async () => await ProcessSaleAsync(), CanProcessSale);
            ClearCartCommand = new RelayCommand(async () => await ClearCartAsync());
            ShowPdf417Command = new RelayCommand(async () => await ShowPdf417Async());
            
            // Actualizar totales cuando cambie el carrito
            CartItems.CollectionChanged += (s, e) => UpdateTotals();
        }

        private ObservableCollection<IDetail> _cartItems = new();
        public ObservableCollection<IDetail> CartItems
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

        private int _selectedDteType = 0; // No default selection
        public int SelectedDteType
        {
            get => _selectedDteType;
            set
            {
                if (SetProperty(ref _selectedDteType, value))
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

        private Bitmap? _pdf417Bitmap;
        public Bitmap? Pdf417Bitmap
        {
            get => _pdf417Bitmap;
            set
            {
                if (SetProperty(ref _pdf417Bitmap, value))
                {
                    OnPropertyChanged(nameof(IsPdf417BitmapVisible));
                    OnPropertyChanged(nameof(Pdf417ImageSource));
                }
            }
        }

        private bool _isPdf417Visible;
        public bool IsPdf417Visible
        {
            get => _isPdf417Visible;
            set => SetProperty(ref _isPdf417Visible, value);
        }

        public bool IsPdf417BitmapVisible => Pdf417Bitmap != null;

        /// <summary>
        /// Propiedad que convierte el Bitmap a BitmapImage para binding directo en XAML
        /// </summary>
        public BitmapImage? Pdf417ImageSource => ConvertBitmapToBitmapImage(Pdf417Bitmap);

        private Guid? _lastProcessedSaleId;
        public Guid? LastProcessedSaleId
        {
            get => _lastProcessedSaleId;
            set => SetProperty(ref _lastProcessedSaleId, value);
        }

        public string CurrentDateTime => DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm");

        // Propiedades formateadas para la UI
        public string FormattedSubtotal => Subtotal.ToString("C", new System.Globalization.CultureInfo("es-CL"));
        public string FormattedTax => Tax.ToString("C", new System.Globalization.CultureInfo("es-CL"));
        public string FormattedTotal => Total.ToString("C", new System.Globalization.CultureInfo("es-CL"));

        // Comandos
        public ICommand AddProductCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ProcessSaleCommand { get; }
        public ICommand ClearCartCommand { get; }
        public ICommand ShowPdf417Command { get; }

        internal async Task AddProductAsync()
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
                    var newItem = _detailFactory.CreateDetail(
                        product.Name,
                        1,
                        product.SalePrice,
                        product.Exenta ? 0 : 0.19 // Asumir IVA 19% si no está exento
                    );

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

        internal async Task RemoveItemAsync(IDetail? item)
        {
            if (item != null && CartItems.Contains(item))
            {
                CartItems.Remove(item);
                UpdateTotals();
            }
        }

        internal bool CanProcessSale()
        {
            var validDteTypes = new[] { 33, 34, 39, 41 };
            return CartItems.Count > 0 &&
                   !string.IsNullOrEmpty(SelectedPaymentMethod) &&
                   validDteTypes.Contains(SelectedDteType) &&
                   !IsBusy;
        }

        internal async Task ProcessSaleAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ClearError();

                // Crear la venta usando el servicio
                var sale = new Core.Domain.Entities.Sale
                {
                    Date = DateTime.Now,
                    Total = Total,
                    Tax = Tax,
                    State = false // Se completará con DTE
                };

                var details = CartItems.Select(item => new Core.Domain.Entities.Detail
                {
                    Amount = item.Amount,
                    Price = item.Price,
                    Total = item.Total
                    // IdProduct se asignará en el servicio
                }).ToList();

                // Crear la venta
                var createdSale = await _saleService.CreateSaleAsync(sale, details);

                // Completar venta normalmente primero
                var saleCompleted = await _saleService.CompleteSaleAsync(createdSale.Id);
                if (!saleCompleted)
                {
                    SetError("Error al completar la venta");
                    return;
                }

                // Generar DTE
                XDocument? dteXml = null;
                try
                {
                    dteXml = await _dteSaleService.GenerateDteForSaleAsync(createdSale.Id, SelectedDteType);

                    // Actualizar información DTE en la base de datos
                    var folio = await _dteSaleService.GetFolioForSaleAsync(createdSale.Id);
                    var cafId = await _dteSaleService.GetCafIdForSaleAsync(createdSale.Id);
                    var dteXmlString = await _dteSaleService.GetDteXmlForSaleAsync(createdSale.Id);

                    if (folio.HasValue && dteXmlString != null)
                    {
                        await _saleService.UpdateSaleDteInfoAsync(
                            createdSale.Id,
                            folio.Value,
                            SelectedDteType.ToString(),
                            cafId,
                            dteXmlString
                        );
                    }
                }
                catch (Exception ex)
                {
                    SetError($"Error al generar DTE: {ex.Message}");
                    return;
                }

                // Almacenar el ID de la venta procesada
                LastProcessedSaleId = createdSale.Id;
                IsPdf417Visible = true;

                // Mostrar automáticamente el PDF417 del TED
                await ShowPdf417Async();

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

        internal async Task ClearCartAsync()
        {
            CartItems.Clear();
            UpdateTotals();
            SelectedPaymentMethod = string.Empty;
            SelectedDteType = 0; // Reset to no selection
            IsPdf417Visible = false;
            Pdf417Bitmap = null;
            LastProcessedSaleId = null;
        }

        internal async Task ShowPdf417Async()
        {
            if (!LastProcessedSaleId.HasValue) return;

            try
            {
                IsBusy = true;
                ClearError();

                // Generar DTE para obtener el XML
                var dteXml = await _dteSaleService.GenerateDteForSaleAsync(LastProcessedSaleId.Value, SelectedDteType);

                // Generar PDF417 desde el TED
                var pdf417Bitmap = _stampingService.GeneratePdf417Image(dteXml);

                // Mostrar el bitmap
                Pdf417Bitmap = pdf417Bitmap;
            }
            catch (Exception ex)
            {
                SetError($"Error al generar código PDF417: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal void UpdateTotals()
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

        /// <summary>
        /// Convierte un System.Drawing.Bitmap a BitmapImage compatible con WinUI
        /// </summary>
        private static BitmapImage? ConvertBitmapToBitmapImage(Bitmap? bitmap)
        {
            if (bitmap == null) return null;

            try
            {
                using var memory = new MemoryStream();
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(memory.AsRandomAccessStream());
                return bitmapImage;
            }
            catch (Exception)
            {
                // En caso de error, devolver null
                return null;
            }
        }
    }
}