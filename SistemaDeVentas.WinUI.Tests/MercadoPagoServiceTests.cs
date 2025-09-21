using FluentAssertions;
using Moq;
using Moq.Protected;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Infrastructure.Services;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Xunit;

namespace SistemaDeVentas.WinUI.Tests;

/// <summary>
/// Pruebas unitarias para MercadoPagoService
/// </summary>
public class MercadoPagoServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly MercadoPagoSettings _settings;

    public MercadoPagoServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _settings = new MercadoPagoSettings
        {
            AccessToken = "TEST_ACCESS_TOKEN",
            PublicKey = "TEST_PUBLIC_KEY",
            TerminalId = "TERM001",
            BaseUrl = "https://api.mercadopago.com"
        };
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenSettingsIsNull()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new MercadoPagoService(null!));
        exception.ParamName.Should().Be("settings");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenSettingsIsInvalid()
    {
        // Arrange
        var invalidSettings = new MercadoPagoSettings(); // Empty settings

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new MercadoPagoService(invalidSettings));
        exception.Message.Should().Contain("La configuración de MercadoPago no es válida");
    }

    [Fact]
    public async Task ProcessPaymentAsync_ShouldReturnError_WhenPaymentMethodIsNotMercadoPago()
    {
        // Arrange
        var service = new MercadoPagoService(_settings);
        var sale = new Sale { Id = Guid.NewGuid(), Total = 1000 };

        // Act
        var result = await service.ProcessPaymentAsync(sale, "efectivo");

        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("Método de pago no soportado: efectivo");
        result.Amount.Should().Be(1000);
    }

    [Fact]
    public async Task ProcessPaymentAsync_ShouldReturnError_WhenTerminalIdIsNull()
    {
        // Arrange
        var service = new MercadoPagoService(_settings);
        var sale = new Sale { Id = Guid.NewGuid(), Total = 1000 };

        // Act
        var result = await service.ProcessPaymentAsync(sale, "mercadopago", null);

        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("No se especificó un terminal de pago");
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnSuccess_WhenApiCallSucceeds()
    {
        // Arrange
        var orderResponse = new
        {
            id = "123456789",
            qr_code = "https://qr.mercadopago.com/qr/123456789",
            qr_code_base64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg=="
        };

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created,
            Content = new StringContent(JsonSerializer.Serialize(orderResponse))
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var service = new MercadoPagoService(_settings);
        var order = new MercadoPagoOrder
        {
            ExternalReference = "TEST123",
            Title = "Venta Test",
            Amount = 1000,
            Items = new List<MercadoPagoItem>
            {
                new MercadoPagoItem { Title = "Producto Test", Quantity = 1, UnitPrice = 1000, TotalAmount = 1000 }
            }
        };

        // Act
        var result = await service.CreateOrderAsync(order, "TERM001");

        // Assert
        result.Success.Should().BeTrue();
        result.OrderId.Should().Be("123456789");
        result.QrCode.Should().Be("https://qr.mercadopago.com/qr/123456789");
        result.QrCodeBase64.Should().Be("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==");
    }

    [Fact]
    public async Task CreateOrderAsync_ShouldReturnError_WhenApiCallFails()
    {
        // Arrange
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent("{\"message\":\"Invalid request\"}")
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var service = new MercadoPagoService(_settings);
        var order = new MercadoPagoOrder { Amount = 1000 };

        // Act
        var result = await service.CreateOrderAsync(order, "TERM001");

        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Contain("Error API MercadoPago");
    }

    [Fact]
    public async Task GetOrderStatusAsync_ShouldReturnStatus_WhenApiCallSucceeds()
    {
        // Arrange
        var statusResponse = new
        {
            id = "123456789",
            status = "paid",
            total_paid_amount = 1000.0,
            payment_method = "credit_card",
            date_last_updated = "2024-01-01T12:00:00Z"
        };

        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(statusResponse))
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var service = new MercadoPagoService(_settings);

        // Act
        var result = await service.GetOrderStatusAsync("123456789");

        // Assert
        result.OrderId.Should().Be("123456789");
        result.Status.Should().Be("paid");
        result.PaidAmount.Should().Be(1000);
        result.PaymentMethod.Should().Be("credit_card");
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReturnTrue_WhenApiCallSucceeds()
    {
        // Arrange
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var service = new MercadoPagoService(_settings);

        // Act
        var result = await service.CancelOrderAsync("123456789");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReturnFalse_WhenApiCallFails()
    {
        // Arrange
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var service = new MercadoPagoService(_settings);

        // Act
        var result = await service.CancelOrderAsync("123456789");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void MercadoPagoSettings_IsValid_ShouldReturnTrue_WhenAllRequiredFieldsAreSet()
    {
        // Arrange
        var settings = new MercadoPagoSettings
        {
            AccessToken = "TEST_TOKEN",
            PublicKey = "TEST_KEY",
            TerminalId = "TERM001"
        };

        // Act
        var isValid = settings.IsValid();

        // Assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void MercadoPagoSettings_IsValid_ShouldReturnFalse_WhenAccessTokenIsMissing()
    {
        // Arrange
        var settings = new MercadoPagoSettings
        {
            PublicKey = "TEST_KEY",
            TerminalId = "TERM001"
        };

        // Act
        var isValid = settings.IsValid();

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void MercadoPagoSettings_IsValid_ShouldReturnFalse_WhenPublicKeyIsMissing()
    {
        // Arrange
        var settings = new MercadoPagoSettings
        {
            AccessToken = "TEST_TOKEN",
            TerminalId = "TERM001"
        };

        // Act
        var isValid = settings.IsValid();

        // Assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void MercadoPagoSettings_IsValid_ShouldReturnFalse_WhenTerminalIdIsMissing()
    {
        // Arrange
        var settings = new MercadoPagoSettings
        {
            AccessToken = "TEST_TOKEN",
            PublicKey = "TEST_KEY"
        };

        // Act
        var isValid = settings.IsValid();

        // Assert
        isValid.Should().BeFalse();
    }
}