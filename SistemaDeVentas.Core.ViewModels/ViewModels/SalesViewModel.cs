using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.UI.Xaml.Media.Imaging;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
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
        private readonly IMercadoPagoService _mercadoPagoService;
        private readonly PrintViewModel _printViewModel;

        public SalesViewModel(ISaleService saleService, IProductService productService, IUserService userService,
                              IDteSaleService dteSaleService, IPdf417Service pdf417Service, IStampingService stampingService,
                              IDetailFactory detailFactory, IMercadoPagoService mercadoPagoService, PrintViewModel printViewModel)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _dteSaleService = dteSaleService ?? throw new ArgumentNullException(nameof(dteSaleService));
            _pdf417Service = pdf417Service ?? throw new ArgumentNullException(nameof(pdf417Service));
            _stampingService = stampingService ?? throw new ArgumentNullException(nameof(stampingService));
            _detailFactory = detailFactory ?? throw new ArgumentNullException(nameof(detailFactory));
            _mercadoPagoService = mercadoPagoService ?? throw new ArgumentNullException(nameof(mercadoPagoService));
            _printViewModel = printViewModel ?? throw new ArgumentNullException(nameof(printViewModel));

            Title = "Punto de Venta";
            CartItems = new ObservableCollection<IDetail>();

            AddProductCommand = new RelayCommand(async () => await AddProductAsync());
            RemoveItemCommand = new RelayCommand<IDetail>(async (item) => await RemoveItemAsync(item));
            ProcessSaleCommand = new RelayCommand(async () => await ProcessSaleAsync(), CanProcessSale);
            ProcessMercadoPagoCommand = new RelayCommand(async () => await ProcessMercadoPagoAsync(), CanProcessMercadoPago);
            ClearCartCommand = new RelayCommand(async () => await ClearCartAsync());
            ShowPdf417Command = new RelayCommand(async () => await ShowPdf417Async());
            CancelMercadoPagoCommand = new RelayCommand(async () => await CancelMercadoPagoAsync(), CanCancelMercadoPago);
            
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

        // Configuración de impresión automática
        private bool _autoPrintEnabled = true; // TODO: Cargar desde configuración
        public bool AutoPrintEnabled
        {
            get => _autoPrintEnabled;
            set => SetProperty(ref _autoPrintEnabled, value);
        }

        private bool _printReceiptAfterSale = true;
        public bool PrintReceiptAfterSale
        {
            get => _printReceiptAfterSale;
            set => SetProperty(ref _printReceiptAfterSale, value);
        }

        private bool _printInvoiceAfterDteGeneration = true;
        public bool PrintInvoiceAfterDteGeneration
        {
            get => _printInvoiceAfterDteGeneration;
            set => SetProperty(ref _printInvoiceAfterDteGeneration, value);
        }

        public string CurrentDateTime => DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm");

        // Propiedades formateadas para la UI
        public string FormattedSubtotal => Subtotal.ToString("C", new System.Globalization.CultureInfo("es-CL"));
        public string FormattedTax => Tax.ToString("C", new System.Globalization.CultureInfo("es-CL"));
        public string FormattedTotal => Total.ToString("C", new System.Globalization.CultureInfo("es-CL"));

        // Propiedades MercadoPago
        private string _mercadoPagoQrCode;
        public string MercadoPagoQrCode
        {
            get => _mercadoPagoQrCode;
            set => SetProperty(ref _mercadoPagoQrCode, value);
        }

        private string _mercadoPagoOrderId;
        public string MercadoPagoOrderId
        {
            get => _mercadoPagoOrderId;
            set => SetProperty(ref _mercadoPagoOrderId, value);
        }

        private string _mercadoPagoStatus;
        public string MercadoPagoStatus
        {
            get => _mercadoPagoStatus;
            set
            {
                if (SetProperty(ref _mercadoPagoStatus, value))
                {
                    OnPropertyChanged(nameof(IsMercadoPagoPaymentInProgress));
                    OnPropertyChanged(nameof(IsMercadoPagoPaymentCompleted));
                    OnPropertyChanged(nameof(CanCancelMercadoPago));
                }
            }
        }

        public bool IsMercadoPagoPaymentInProgress => MercadoPagoStatus == "pending";
        public bool IsMercadoPagoPaymentCompleted => MercadoPagoStatus == "paid";

        // Comandos
        public ICommand AddProductCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ProcessSaleCommand { get; }
        public ICommand ProcessMercadoPagoCommand { get; }
        public ICommand ClearCartCommand { get; }
        public ICommand ShowPdf417Command { get; }
        public ICommand CancelMercadoPagoCommand { get; }

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

            string? dteXmlString = null;

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
                    dteXmlString = await _dteSaleService.GetDteXmlForSaleAsync(createdSale.Id);

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

                // Imprimir automáticamente si está habilitado
                await PrintSaleAsync(createdSale, dteXmlString);
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

            // Limpiar estado MercadoPago
            MercadoPagoQrCode = string.Empty;
            MercadoPagoOrderId = string.Empty;
            MercadoPagoStatus = string.Empty;
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
            ((RelayCommand)ProcessMercadoPagoCommand).RaiseCanExecuteChanged();
        }

        internal bool CanProcessMercadoPago()
        {
            return CartItems.Count > 0 && Total > 0 && !IsBusy && !IsMercadoPagoPaymentInProgress;
        }

        internal async Task ProcessMercadoPagoAsync()
        {
            if (IsBusy || IsMercadoPagoPaymentInProgress) return;

            try
            {
                IsBusy = true;
                ClearError();

                // Crear orden temporal para MercadoPago
                var tempSale = new Core.Domain.Entities.Sale
                {
                    Date = DateTime.Now,
                    Total = Total,
                    Tax = Tax,
                    State = false
                };

                // Procesar pago con MercadoPago
                var paymentResult = await _mercadoPagoService.ProcessPaymentAsync(tempSale, "mercadopago", null);

                if (paymentResult.Success)
                {
                    // Pago exitoso - proceder con la venta completa
                    await ProcessSaleAfterMercadoPagoPayment(paymentResult);
                }
                else
                {
                    SetError($"Error en pago MercadoPago: {paymentResult.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                SetError($"Error procesando pago MercadoPago: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ProcessSaleAfterMercadoPagoPayment(PaymentResult paymentResult)
        {
            try
            {
                // Crear la venta completa
                var sale = new Core.Domain.Entities.Sale
                {
                    Date = DateTime.Now,
                    Total = Total,
                    Tax = Tax,
                    State = false
                };

                var details = CartItems.Select(item => new Core.Domain.Entities.Detail
                {
                    Amount = item.Amount,
                    Price = item.Price,
                    Total = item.Total
                }).ToList();

                var createdSale = await _saleService.CreateSaleAsync(sale, details);
                var saleCompleted = await _saleService.CompleteSaleAsync(createdSale.Id);

                if (!saleCompleted)
                {
                    SetError("Error al completar la venta después del pago");
                    return;
                }

                // Actualizar información de pago en la venta
                await _saleService.UpdateSalePaymentInfoAsync(createdSale.Id, "mercadopago", paymentResult.TransactionId);

                // Procesar DTE normalmente
                var dteXml = await ProcessDteForSale(createdSale.Id);

                // Imprimir automáticamente si está habilitado
                await PrintSaleAsync(createdSale, dteXml);

                // Limpiar estado MercadoPago
                MercadoPagoQrCode = string.Empty;
                MercadoPagoOrderId = string.Empty;
                MercadoPagoStatus = string.Empty;

                // Mostrar mensaje de éxito
                // TODO: Implementar notificación de éxito
            }
            catch (Exception ex)
            {
                SetError($"Error finalizando venta: {ex.Message}");
            }
        }

        private async Task<string?> ProcessDteForSale(Guid saleId)
        {
            try
            {
                var dteXml = await _dteSaleService.GenerateDteForSaleAsync(saleId, SelectedDteType);

                var folio = await _dteSaleService.GetFolioForSaleAsync(saleId);
                var cafId = await _dteSaleService.GetCafIdForSaleAsync(saleId);
                var dteXmlString = await _dteSaleService.GetDteXmlForSaleAsync(saleId);

                if (folio.HasValue && dteXmlString != null)
                {
                    await _saleService.UpdateSaleDteInfoAsync(
                        saleId,
                        folio.Value,
                        SelectedDteType.ToString(),
                        cafId,
                        dteXmlString
                    );
                }

                LastProcessedSaleId = saleId;
                IsPdf417Visible = true;
                await ShowPdf417Async();

                return dteXmlString;
            }
            catch (Exception ex)
            {
                SetError($"Error generando DTE: {ex.Message}");
                return null;
            }
        }

        internal bool CanCancelMercadoPago()
        {
            return !string.IsNullOrEmpty(MercadoPagoOrderId) && IsMercadoPagoPaymentInProgress;
        }

        internal async Task CancelMercadoPagoAsync()
        {
            if (string.IsNullOrEmpty(MercadoPagoOrderId)) return;

            try
            {
                IsBusy = true;
                ClearError();

                var cancelled = await _mercadoPagoService.CancelOrderAsync(MercadoPagoOrderId);

                if (cancelled)
                {
                    MercadoPagoStatus = "cancelled";
                    MercadoPagoQrCode = string.Empty;
                    SetError("Pago MercadoPago cancelado");
                }
                else
                {
                    SetError("Error al cancelar el pago MercadoPago");
                }
            }
            catch (Exception ex)
            {
                SetError($"Error cancelando pago: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Convierte XML DTE a objeto DteDocument
        /// </summary>
        public async Task<DteDocument?> GetDteDocumentAsync(string dteXml)
        {
            if (string.IsNullOrEmpty(dteXml))
                return null;

            try
            {
                var serializer = new XmlSerializer(typeof(DteDocument));
                using var stringReader = new System.IO.StringReader(dteXml);
                return (DteDocument?)serializer.Deserialize(stringReader);
            }
            catch (Exception ex)
            {
                SetError($"Error al deserializar DTE XML: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene la venta procesada actualmente para impresión
        /// </summary>
        public async Task<Sale?> GetProcessedSaleAsync()
        {
            if (!LastProcessedSaleId.HasValue) return null;

            try
            {
                return await _saleService.GetSaleByIdAsync(LastProcessedSaleId.Value);
            }
            catch (Exception ex)
            {
                SetError($"Error al obtener venta procesada: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Imprime automáticamente la venta y/o DTE según configuración
        /// </summary>
        private async Task PrintSaleAsync(Sale sale, string? dteXml = null)
        {
            if (!AutoPrintEnabled) return;

            try
            {
                // Imprimir boleta si está habilitado
                if (PrintReceiptAfterSale)
                {
                    _printViewModel.CurrentSale = sale;
                    _printViewModel.PrintReceiptCommand.Execute(null);
                }

                // Imprimir factura DTE si está habilitado y hay XML DTE
                if (PrintInvoiceAfterDteGeneration && !string.IsNullOrEmpty(dteXml))
                {
                    var dteDocument = await GetDteDocumentAsync(dteXml);
                    if (dteDocument != null)
                    {
                        _printViewModel.CurrentDteDocument = dteDocument;
                        // Generar QR code data (puede ser el TED o folio)
                        _printViewModel.QrCodeData = GenerateQrCodeData(dteDocument);
                        _printViewModel.PrintInvoiceCommand.Execute(null);
                    }
                }
            }
            catch (Exception ex)
            {
                // No interrumpir el flujo de venta por errores de impresión
                SetError($"Error en impresión automática (venta continúa): {ex.Message}");
            }
        }

        /// <summary>
        /// Genera datos QR para el DTE (simplificado)
        /// </summary>
        private string GenerateQrCodeData(DteDocument dteDocument)
        {
            // Implementar lógica para generar QR code según estándar DTE
            // Por ahora, devolver el folio
            return dteDocument.IdDoc.Folio.ToString();
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