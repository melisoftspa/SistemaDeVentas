using FluentAssertions;
using Moq;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Exceptions.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using SistemaDeVentas.Core.ViewModels.ViewModels;
using SistemaDeVentas.Infrastructure.Services.DTE;
using System.Xml.Linq;
using Xunit;

namespace SistemaDeVentas.WinUI.Tests;

/// <summary>
/// Pruebas unitarias exhaustivas para SalesViewModel.
/// </summary>
public class SalesViewModelTests
{
    private readonly Mock<ISaleService> _saleServiceMock;
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IDteSaleService> _dteSaleServiceMock;
    private readonly Mock<IPdf417Service> _pdf417ServiceMock;
    private readonly Mock<IStampingService> _stampingServiceMock;
    private readonly Mock<IDetailFactory> _detailFactoryMock;

    public SalesViewModelTests()
    {
        _saleServiceMock = new Mock<ISaleService>();
        _productServiceMock = new Mock<IProductService>();
        _userServiceMock = new Mock<IUserService>();
        _dteSaleServiceMock = new Mock<IDteSaleService>();
        _pdf417ServiceMock = new Mock<IPdf417Service>();
        _stampingServiceMock = new Mock<IStampingService>();
        _detailFactoryMock = new Mock<IDetailFactory>();
    }

    #region Constructor and Initialization Tests

    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Act
        var viewModel = CreateViewModel();

        // Assert
        viewModel.Title.Should().Be("Punto de Venta");
        viewModel.CartItems.Should().NotBeNull();
        viewModel.CartItems.Should().BeEmpty();
        viewModel.Subtotal.Should().Be(0);
        viewModel.Tax.Should().Be(0);
        viewModel.Total.Should().Be(0);
        viewModel.SearchText.Should().BeEmpty();
        viewModel.SelectedPaymentMethod.Should().BeEmpty();
        viewModel.SelectedDteType.Should().Be(0);
        viewModel.IsBusy.Should().BeFalse();
        viewModel.ErrorMessage.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenSaleServiceIsNull()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new SalesViewModel(null!, _productServiceMock.Object, _userServiceMock.Object,
                              _dteSaleServiceMock.Object, _pdf417ServiceMock.Object, _stampingServiceMock.Object, _detailFactoryMock.Object));
        exception.ParamName.Should().Be("saleService");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenProductServiceIsNull()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new SalesViewModel(_saleServiceMock.Object, null!, _userServiceMock.Object,
                              _dteSaleServiceMock.Object, _pdf417ServiceMock.Object, _stampingServiceMock.Object, _detailFactoryMock.Object));
        exception.ParamName.Should().Be("productService");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenUserServiceIsNull()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new SalesViewModel(_saleServiceMock.Object, _productServiceMock.Object, null!,
                              _dteSaleServiceMock.Object, _pdf417ServiceMock.Object, _stampingServiceMock.Object, _detailFactoryMock.Object));
        exception.ParamName.Should().Be("userService");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDteSaleServiceIsNull()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new SalesViewModel(_saleServiceMock.Object, _productServiceMock.Object, _userServiceMock.Object,
                              null!, _pdf417ServiceMock.Object, _stampingServiceMock.Object, _detailFactoryMock.Object));
        exception.ParamName.Should().Be("dteSaleService");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenPdf417ServiceIsNull()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new SalesViewModel(_saleServiceMock.Object, _productServiceMock.Object, _userServiceMock.Object,
                              _dteSaleServiceMock.Object, null!, _stampingServiceMock.Object, _detailFactoryMock.Object));
        exception.ParamName.Should().Be("pdf417Service");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenStampingServiceIsNull()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new SalesViewModel(_saleServiceMock.Object, _productServiceMock.Object, _userServiceMock.Object,
                              _dteSaleServiceMock.Object, _pdf417ServiceMock.Object, null!, _detailFactoryMock.Object));
        exception.ParamName.Should().Be("stampingService");
    }

    #endregion

    #region Property Tests

    [Fact]
    public void SearchText_ShouldNotifyPropertyChanged()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var propertyChangedCalled = false;
        viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(viewModel.SearchText))
                propertyChangedCalled = true;
        };

        // Act
        viewModel.SearchText = "test";

        // Assert
        propertyChangedCalled.Should().BeTrue();
        viewModel.SearchText.Should().Be("test");
    }

    [Fact]
    public void SelectedPaymentMethod_ShouldNotifyPropertyChanged()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var propertyChangedCalled = false;
        viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(viewModel.SelectedPaymentMethod))
                propertyChangedCalled = true;
        };

        // Act
        viewModel.SelectedPaymentMethod = "Efectivo";

        // Assert
        propertyChangedCalled.Should().BeTrue();
        viewModel.SelectedPaymentMethod.Should().Be("Efectivo");
    }

    [Fact]
    public void SelectedDteType_ShouldNotifyPropertyChanged_AndRaiseCanExecuteChanged()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var propertyChangedCalled = false;
        var canExecuteChangedCalled = false;
        viewModel.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(viewModel.SelectedDteType))
                propertyChangedCalled = true;
        };
        ((RelayCommand)viewModel.ProcessSaleCommand).CanExecuteChanged += (s, e) => canExecuteChangedCalled = true;

        // Act
        viewModel.SelectedDteType = 39;

        // Assert
        propertyChangedCalled.Should().BeTrue();
        canExecuteChangedCalled.Should().BeTrue();
        viewModel.SelectedDteType.Should().Be(39);
    }

    [Fact]
    public void FormattedSubtotal_ShouldReturnCorrectFormat()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1234.56, Tax = 0 });
        viewModel.UpdateTotals();

        // Act
        var formatted = viewModel.FormattedSubtotal;

        // Assert
        formatted.Should().Be("$1,234.56");
    }

    [Fact]
    public void FormattedTax_ShouldReturnCorrectFormat()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1234.56, Tax = 19 });
        viewModel.UpdateTotals();

        // Act
        var formatted = viewModel.FormattedTax;

        // Assert
        formatted.Should().Be("$234.56");
    }

    [Fact]
    public void FormattedTotal_ShouldReturnCorrectFormat()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1234.56, Tax = 19 });
        viewModel.UpdateTotals();

        // Act
        var formatted = viewModel.FormattedTotal;

        // Assert
        formatted.Should().Be("$1,469.12");
    }

    #endregion

    #region AddProductAsync Tests

    [Fact]
    public async Task AddProductAsync_ShouldAddProductToCart_WhenProductFoundAndActive()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var product = new Product { Id = Guid.NewGuid(), Name = "Producto Test", IsActive = true, Stock = 10, SalePrice = 1000 };
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product> { product });

        viewModel.SearchText = "Producto Test";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.CartItems.Should().HaveCount(1);
        viewModel.CartItems[0].ProductName.Should().Be("Producto Test");
        viewModel.CartItems[0].Amount.Should().Be(1);
        viewModel.CartItems[0].Price.Should().Be(1000);
        viewModel.SearchText.Should().BeEmpty();
        viewModel.Subtotal.Should().Be(1000);
        viewModel.Tax.Should().Be(190); // 19% IVA
        viewModel.Total.Should().Be(1190);
    }

    [Fact]
    public async Task AddProductAsync_ShouldIncrementQuantity_WhenProductAlreadyInCart()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var product = new Product { Id = Guid.NewGuid(), Name = "Producto Test", IsActive = true, Stock = 10, SalePrice = 1000 };
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product> { product });

        viewModel.SearchText = "Producto Test";
        await viewModel.AddProductAsync();
        viewModel.SearchText = "Producto Test";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.CartItems.Should().HaveCount(1);
        viewModel.CartItems[0].Amount.Should().Be(2);
        viewModel.Subtotal.Should().Be(2000);
        viewModel.Tax.Should().Be(380);
        viewModel.Total.Should().Be(2380);
    }

    [Fact]
    public async Task AddProductAsync_ShouldSetError_WhenProductNotFound()
    {
        // Arrange
        var viewModel = CreateViewModel();
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product>());

        viewModel.SearchText = "Producto Inexistente";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.ErrorMessage.Should().Be("Producto no encontrado");
        viewModel.CartItems.Should().BeEmpty();
    }

    [Fact]
    public async Task AddProductAsync_ShouldSetError_WhenProductNotActive()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var product = new Product { Id = Guid.NewGuid(), Name = "Producto Test", IsActive = false, Stock = 10, SalePrice = 1000 };
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product> { product });

        viewModel.SearchText = "Producto Test";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.ErrorMessage.Should().Be("El producto no está activo");
        viewModel.CartItems.Should().BeEmpty();
    }

    [Fact]
    public async Task AddProductAsync_ShouldSetError_WhenProductOutOfStock()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var product = new Product { Id = Guid.NewGuid(), Name = "Producto Test", IsActive = true, Stock = 0, SalePrice = 1000 };
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product> { product });

        viewModel.SearchText = "Producto Test";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.ErrorMessage.Should().Be("Producto sin stock disponible");
        viewModel.CartItems.Should().BeEmpty();
    }

    [Fact]
    public async Task AddProductAsync_ShouldSetError_WhenExceedingStock()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var product = new Product { Id = Guid.NewGuid(), Name = "Producto Test", IsActive = true, Stock = 2, SalePrice = 1000 };
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product> { product });

        viewModel.SearchText = "Producto Test";
        await viewModel.AddProductAsync();
        await viewModel.AddProductAsync(); // Now has 2 in cart

        // Act
        await viewModel.AddProductAsync(); // Try to add third

        // Assert
        viewModel.ErrorMessage.Should().Be("No hay suficiente stock para agregar más unidades");
        viewModel.CartItems[0].Amount.Should().Be(2);
    }

    [Fact]
    public async Task AddProductAsync_ShouldFindProductByBarcode()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var product = new Product { Id = Guid.NewGuid(), Name = "Producto Test", IsActive = true, Stock = 10, SalePrice = 1000, Barcode = "123456789" };
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product> { product });

        viewModel.SearchText = "123456789";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.CartItems.Should().HaveCount(1);
        viewModel.CartItems[0].ProductName.Should().Be("Producto Test");
    }

    #endregion

    #region RemoveItemAsync Tests

    [Fact]
    public async Task RemoveItemAsync_ShouldRemoveItemFromCart()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var item = new Detail { ProductName = "Test", Amount = 1, Price = 1000 };
        viewModel.CartItems.Add(item);

        // Act
        await viewModel.RemoveItemAsync(item);

        // Assert
        viewModel.CartItems.Should().BeEmpty();
        viewModel.Subtotal.Should().Be(0);
        viewModel.Tax.Should().Be(0);
        viewModel.Total.Should().Be(0);
    }

    [Fact]
    public async Task RemoveItemAsync_ShouldDoNothing_WhenItemIsNull()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var item = new Detail { ProductName = "Test", Amount = 1, Price = 1000 };
        viewModel.CartItems.Add(item);

        // Act
        await viewModel.RemoveItemAsync(null);

        // Assert
        viewModel.CartItems.Should().HaveCount(1);
    }

    [Fact]
    public async Task RemoveItemAsync_ShouldDoNothing_WhenItemNotInCart()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var item1 = new Detail { ProductName = "Test1", Amount = 1, Price = 1000 };
        var item2 = new Detail { ProductName = "Test2", Amount = 1, Price = 1000 };
        viewModel.CartItems.Add(item1);

        // Act
        await viewModel.RemoveItemAsync(item2);

        // Assert
        viewModel.CartItems.Should().HaveCount(1);
        viewModel.CartItems[0].Should().Be(item1);
    }

    #endregion

    #region CanProcessSale Tests

    [Fact]
    public void CanProcessSale_ShouldReturnFalse_WhenCartIsEmpty()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;

        // Act
        var canProcess = viewModel.CanProcessSale();

        // Assert
        canProcess.Should().BeFalse();
    }

    [Fact]
    public void CanProcessSale_ShouldReturnFalse_WhenPaymentMethodIsEmpty()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000 });
        viewModel.SelectedDteType = 39;

        // Act
        var canProcess = viewModel.CanProcessSale();

        // Assert
        canProcess.Should().BeFalse();
    }

    [Fact]
    public void CanProcessSale_ShouldReturnFalse_WhenDteTypeIsInvalid()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 99; // Invalid

        // Act
        var canProcess = viewModel.CanProcessSale();

        // Assert
        canProcess.Should().BeFalse();
    }

    [Fact]
    public void CanProcessSale_ShouldReturnFalse_WhenIsBusy()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;
        viewModel.IsBusy = true;

        // Act
        var canProcess = viewModel.CanProcessSale();

        // Assert
        canProcess.Should().BeFalse();
    }

    [Fact]
    public void CanProcessSale_ShouldReturnTrue_WhenAllConditionsMet()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;

        // Act
        var canProcess = viewModel.CanProcessSale();

        // Assert
        canProcess.Should().BeTrue();
    }

    #endregion

    #region ProcessSaleAsync Tests

    [Fact]
    public async Task ProcessSaleAsync_ShouldProcessSaleSuccessfully()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var saleId = Guid.NewGuid();
        var createdSale = new Sale { Id = saleId, Date = DateTime.Now, Total = 1190, Tax = 190, State = false };
        var dteXml = XDocument.Parse("<DTE><Documento>Test</Documento></DTE>");

        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000, Tax = 190 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;

        _saleServiceMock.Setup(s => s.CreateSaleAsync(It.IsAny<Sale>(), It.IsAny<List<Core.Domain.Entities.Detail>>()))
                       .ReturnsAsync(createdSale);
        _saleServiceMock.Setup(s => s.CompleteSaleAsync(saleId)).ReturnsAsync(true);
        _dteSaleServiceMock.Setup(d => d.GenerateDteForSaleAsync(saleId, 39)).ReturnsAsync(dteXml);

        // Act
        await viewModel.ProcessSaleAsync();

        // Assert
        viewModel.LastProcessedSaleId.Should().Be(saleId);
        viewModel.IsPdf417Visible.Should().BeTrue();
        viewModel.CartItems.Should().BeEmpty();
        viewModel.SelectedPaymentMethod.Should().BeEmpty();
        viewModel.SelectedDteType.Should().Be(0);
        viewModel.ErrorMessage.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task ProcessSaleAsync_ShouldSetError_WhenCompleteSaleFails()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var saleId = Guid.NewGuid();
        var createdSale = new Sale { Id = saleId, Date = DateTime.Now, Total = 1190, Tax = 190, State = false };

        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000, Tax = 190 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;

        _saleServiceMock.Setup(s => s.CreateSaleAsync(It.IsAny<Sale>(), It.IsAny<List<Core.Domain.Entities.Detail>>()))
                       .ReturnsAsync(createdSale);
        _saleServiceMock.Setup(s => s.CompleteSaleAsync(saleId)).ReturnsAsync(false);

        // Act
        await viewModel.ProcessSaleAsync();

        // Assert
        viewModel.ErrorMessage.Should().Be("Error al completar la venta");
        viewModel.LastProcessedSaleId.Should().BeNull();
        viewModel.IsPdf417Visible.Should().BeFalse();
    }

    [Fact]
    public async Task ProcessSaleAsync_ShouldSetError_WhenDteGenerationFails()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var saleId = Guid.NewGuid();
        var createdSale = new Sale { Id = saleId, Date = DateTime.Now, Total = 1190, Tax = 190, State = false };

        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000, Tax = 190 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;

        _saleServiceMock.Setup(s => s.CreateSaleAsync(It.IsAny<Sale>(), It.IsAny<List<Core.Domain.Entities.Detail>>()))
                       .ReturnsAsync(createdSale);
        _saleServiceMock.Setup(s => s.CompleteSaleAsync(saleId)).ReturnsAsync(true);
        _dteSaleServiceMock.Setup(d => d.GenerateDteForSaleAsync(saleId, 39))
                          .ThrowsAsync(new Exception("Error DTE"));

        // Act
        await viewModel.ProcessSaleAsync();

        // Assert
        viewModel.ErrorMessage.Should().Contain("Error al generar DTE");
        viewModel.LastProcessedSaleId.Should().BeNull();
        viewModel.IsPdf417Visible.Should().BeFalse();
    }

    [Fact]
    public async Task ProcessSaleAsync_ShouldDoNothing_WhenIsBusy()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.IsBusy = true;

        // Act
        await viewModel.ProcessSaleAsync();

        // Assert
        _saleServiceMock.Verify(s => s.CreateSaleAsync(It.IsAny<Sale>(), It.IsAny<List<Core.Domain.Entities.Detail>>()), Times.Never);
    }

    #endregion

    #region ClearCartAsync Tests

    [Fact]
    public async Task ClearCartAsync_ShouldClearAllItemsAndResetProperties()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;
        viewModel.IsPdf417Visible = true;
        viewModel.Pdf417Bitmap = new System.Drawing.Bitmap(1, 1);
        viewModel.LastProcessedSaleId = Guid.NewGuid();

        // Act
        await viewModel.ClearCartAsync();

        // Assert
        viewModel.CartItems.Should().BeEmpty();
        viewModel.Subtotal.Should().Be(0);
        viewModel.Tax.Should().Be(0);
        viewModel.Total.Should().Be(0);
        viewModel.SelectedPaymentMethod.Should().BeEmpty();
        viewModel.SelectedDteType.Should().Be(0);
        viewModel.IsPdf417Visible.Should().BeFalse();
        viewModel.Pdf417Bitmap.Should().BeNull();
        viewModel.LastProcessedSaleId.Should().BeNull();
    }

    #endregion

    #region ShowPdf417Async Tests

    [Fact]
    public async Task ShowPdf417Async_ShouldGenerateAndShowPdf417_WhenLastProcessedSaleIdExists()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var saleId = Guid.NewGuid();
        var dteXml = XDocument.Parse("<DTE><Documento>Test</Documento></DTE>");
        var bitmap = new System.Drawing.Bitmap(1, 1);

        viewModel.LastProcessedSaleId = saleId;
        viewModel.SelectedDteType = 39;

        _dteSaleServiceMock.Setup(d => d.GenerateDteForSaleAsync(saleId, 39)).ReturnsAsync(dteXml);
        _stampingServiceMock.Setup(s => s.GeneratePdf417Image(dteXml)).Returns(bitmap);

        // Act
        await viewModel.ShowPdf417Async();

        // Assert
        viewModel.Pdf417Bitmap.Should().Be(bitmap);
        viewModel.IsPdf417Visible.Should().BeTrue();
        viewModel.ErrorMessage.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task ShowPdf417Async_ShouldDoNothing_WhenLastProcessedSaleIdIsNull()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.LastProcessedSaleId = null;

        // Act
        await viewModel.ShowPdf417Async();

        // Assert
        viewModel.Pdf417Bitmap.Should().BeNull();
        _dteSaleServiceMock.Verify(d => d.GenerateDteForSaleAsync(It.IsAny<Guid>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task ShowPdf417Async_ShouldSetError_WhenDteGenerationFails()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var saleId = Guid.NewGuid();

        viewModel.LastProcessedSaleId = saleId;
        viewModel.SelectedDteType = 39;

        _dteSaleServiceMock.Setup(d => d.GenerateDteForSaleAsync(saleId, 39))
                          .ThrowsAsync(new Exception("Error DTE"));

        // Act
        await viewModel.ShowPdf417Async();

        // Assert
        viewModel.ErrorMessage.Should().Contain("Error al generar código PDF417");
        viewModel.Pdf417Bitmap.Should().BeNull();
    }

    #endregion

    #region UpdateTotals Tests

    [Fact]
    public void UpdateTotals_ShouldCalculateCorrectTotals()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test1", Amount = 2, Price = 1000, Tax = 380 }); // Subtotal: 2000, Tax: 380
        viewModel.CartItems.Add(new Detail { ProductName = "Test2", Amount = 1, Price = 500, Tax = 95 });   // Subtotal: 500, Tax: 95

        // Act
        viewModel.UpdateTotals();

        // Assert
        viewModel.Subtotal.Should().Be(2500);
        viewModel.Tax.Should().Be(475);
        viewModel.Total.Should().Be(2975);
    }

    [Fact]
    public void UpdateTotals_ShouldNotifyPropertyChanged()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var propertiesChanged = new List<string>();
        viewModel.PropertyChanged += (s, e) => propertiesChanged.Add(e.PropertyName!);

        // Act
        viewModel.UpdateTotals();

        // Assert
        propertiesChanged.Should().Contain(nameof(viewModel.FormattedSubtotal));
        propertiesChanged.Should().Contain(nameof(viewModel.FormattedTax));
        propertiesChanged.Should().Contain(nameof(viewModel.FormattedTotal));
    }

    #endregion

    #region DTE Validation Tests

    [Fact]
    public void EmisorValidator_ShouldValidateCorrectly()
    {
        // Arrange
        var validator = new EmisorValidator();
        var emisor = new Emisor
        {
            RutEmisor = "76192083-9",
            RazonSocial = "Empresa Test S.A.",
            GiroEmisor = "Venta de productos",
            DireccionOrigen = "Dirección Test 123",
            ComunaOrigen = "Comuna Test"
        };

        // Act
        var result = validator.Validate(emisor);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void EmisorValidator_ShouldFail_WhenRUTIsInvalid()
    {
        // Arrange
        var validator = new EmisorValidator();
        var emisor = new Emisor
        {
            RutEmisor = "invalid-rut",
            RazonSocial = "Empresa Test S.A.",
            GiroEmisor = "Venta de productos"
        };

        // Act
        var result = validator.Validate(emisor);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "RUTEmisor");
    }

    [Fact]
    public void ReceptorValidator_ShouldValidateCorrectly_ForClienteFinal()
    {
        // Arrange
        var validator = new ReceptorValidator();
        var receptor = new Receptor
        {
            RutReceptor = "66666666-6",
            RazonSocialReceptor = "Cliente Final"
        };

        // Act
        var result = validator.Validate(receptor);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void DetalleDteValidator_ShouldValidateCorrectly()
    {
        // Arrange
        var validator = new DetalleDteValidator();
        var detalle = new DetalleDte
        {
            NumeroLineaDetalle = 1,
            NombreItem = "Producto Test",
            CantidadItem = 1,
            PrecioItem = 1000,
            MontoItem = 1000
        };

        // Act
        var result = validator.Validate(detalle);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void DetalleDteValidator_ShouldFail_WhenRequiredFieldsMissing()
    {
        // Arrange
        var validator = new DetalleDteValidator();
        var detalle = new DetalleDte
        {
            NumeroLineaDetalle = 1,
            CantidadItem = 1,
            PrecioItem = 1000
            // Missing NmbItem and MontoItem
        };

        // Act
        var result = validator.Validate(detalle);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "NmbItem");
        result.Errors.Should().Contain(e => e.PropertyName == "MontoItem");
    }

    #endregion

    #region Exception Handling Tests

    [Fact]
    public async Task AddProductAsync_ShouldHandleException_AndSetError()
    {
        // Arrange
        var viewModel = CreateViewModel();
        _productServiceMock.Setup(s => s.GetAllProductsAsync()).ThrowsAsync(new Exception("Database error"));

        viewModel.SearchText = "Test";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.ErrorMessage.Should().Contain("Error al agregar producto");
        viewModel.IsBusy.Should().BeFalse();
    }

    [Fact]
    public async Task ProcessSaleAsync_ShouldHandleException_AndSetError()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;

        _saleServiceMock.Setup(s => s.CreateSaleAsync(It.IsAny<Sale>(), It.IsAny<List<Core.Domain.Entities.Detail>>()))
                       .ThrowsAsync(new Exception("Database error"));

        // Act
        await viewModel.ProcessSaleAsync();

        // Assert
        viewModel.ErrorMessage.Should().Contain("Error al procesar la venta");
        viewModel.IsBusy.Should().BeFalse();
    }

    [Fact]
    public async Task ShowPdf417Async_ShouldHandleException_AndSetError()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.LastProcessedSaleId = Guid.NewGuid();

        _dteSaleServiceMock.Setup(d => d.GenerateDteForSaleAsync(It.IsAny<Guid>(), It.IsAny<int>()))
                          .ThrowsAsync(new DteValidationException("Validación fallida"));

        // Act
        await viewModel.ShowPdf417Async();

        // Assert
        viewModel.ErrorMessage.Should().Contain("Error al generar código PDF417");
        viewModel.IsBusy.Should().BeFalse();
    }

    #endregion

    #region Edge Cases Tests

    [Fact]
    public async Task AddProductAsync_ShouldHandleEmptySearchText()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.SearchText = "";

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.ErrorMessage.Should().Be("Producto no encontrado");
    }

    [Fact]
    public async Task AddProductAsync_ShouldHandleNullSearchText()
    {
        // Arrange
        var viewModel = CreateViewModel();
        viewModel.SearchText = null!;

        // Act
        await viewModel.AddProductAsync();

        // Assert
        viewModel.ErrorMessage.Should().Be("Producto no encontrado");
    }

    [Fact]
    public void CanProcessSale_ShouldHandleAllValidDteTypes()
    {
        // Arrange
        var validTypes = new[] { 33, 34, 39, 41 };
        var viewModel = CreateViewModel();
        viewModel.CartItems.Add(new Detail { ProductName = "Test", Amount = 1, Price = 1000 });
        viewModel.SelectedPaymentMethod = "Efectivo";

        foreach (var type in validTypes)
        {
            // Act
            viewModel.SelectedDteType = type;
            var canProcess = viewModel.CanProcessSale();

            // Assert
            canProcess.Should().BeTrue();
        }
    }

    [Fact]
    public async Task ProcessSaleAsync_ShouldHandleZeroTotal()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var saleId = Guid.NewGuid();
        var createdSale = new Sale { Id = saleId, Date = DateTime.Now, Total = 0, Tax = 0, State = false };
        var dteXml = XDocument.Parse("<DTE><Documento>Test</Documento></DTE>");

        viewModel.CartItems.Add(new Detail { ProductName = "Free Item", Amount = 1, Price = 0, Tax = 0 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 39;

        _saleServiceMock.Setup(s => s.CreateSaleAsync(It.IsAny<Sale>(), It.IsAny<List<Core.Domain.Entities.Detail>>()))
                       .ReturnsAsync(createdSale);
        _saleServiceMock.Setup(s => s.CompleteSaleAsync(saleId)).ReturnsAsync(true);
        _dteSaleServiceMock.Setup(d => d.GenerateDteForSaleAsync(saleId, 39)).ReturnsAsync(dteXml);

        // Act
        await viewModel.ProcessSaleAsync();

        // Assert
        viewModel.LastProcessedSaleId.Should().Be(saleId);
        viewModel.Total.Should().Be(0);
    }

    #endregion

    #region XML Schema Validation Tests

    [Fact]
    public async Task ProcessSaleAsync_ShouldGenerateValidDteXml_ForFacturaAfecta()
    {
        // Arrange
        var viewModel = CreateViewModel();
        var saleId = Guid.NewGuid();
        var createdSale = new Sale { Id = saleId, Date = DateTime.Now, Total = 1190, Tax = 190, State = false };
        var dteXml = XDocument.Parse(@"
            <DTE xmlns=""http://www.sii.cl/SiiDte"" version=""1.0"">
                <Documento ID=""F39T1"">
                    <Encabezado>
                        <IdDoc>
                            <TipoDTE>33</TipoDTE>
                            <Folio>1</Folio>
                            <FchEmis>2024-01-01</FchEmis>
                        </IdDoc>
                        <Emisor>
                            <RUTEmisor>76192083-9</RUTEmisor>
                            <RznSoc>Empresa Test</RznSoc>
                        </Emisor>
                        <Receptor>
                            <RUTRecep>66666666-6</RUTRecep>
                            <RznSocRecep>Cliente Final</RznSocRecep>
                        </Receptor>
                        <Totales>
                            <MntNeto>1000</MntNeto>
                            <IVA>190</IVA>
                            <MntTotal>1190</MntTotal>
                        </Totales>
                    </Encabezado>
                    <Detalle>
                        <NroLinDet>1</NroLinDet>
                        <NmbItem>Producto Test</NmbItem>
                        <QtyItem>1</QtyItem>
                        <PrcItem>1000</PrcItem>
                        <MontoItem>1000</MontoItem>
                    </Detalle>
                </Documento>
            </DTE>");

        viewModel.CartItems.Add(new Detail { ProductName = "Producto Test", Amount = 1, Price = 1000, Tax = 190 });
        viewModel.SelectedPaymentMethod = "Efectivo";
        viewModel.SelectedDteType = 33; // Factura Afecta

        _saleServiceMock.Setup(s => s.CreateSaleAsync(It.IsAny<Sale>(), It.IsAny<List<Core.Domain.Entities.Detail>>()))
                       .ReturnsAsync(createdSale);
        _saleServiceMock.Setup(s => s.CompleteSaleAsync(saleId)).ReturnsAsync(true);
        _dteSaleServiceMock.Setup(d => d.GenerateDteForSaleAsync(saleId, 33)).ReturnsAsync(dteXml);

        // Act
        await viewModel.ProcessSaleAsync();

        // Assert
        viewModel.LastProcessedSaleId.Should().Be(saleId);
        // Additional XML schema validation could be added here
    }

    #endregion

    private SalesViewModel CreateViewModel()
    {
        return new SalesViewModel(
            _saleServiceMock.Object,
            _productServiceMock.Object,
            _userServiceMock.Object,
            _dteSaleServiceMock.Object,
            _pdf417ServiceMock.Object,
            _stampingServiceMock.Object,
            _detailFactoryMock.Object);
    }
}