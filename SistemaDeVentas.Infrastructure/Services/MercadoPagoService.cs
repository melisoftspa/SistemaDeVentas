using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Infrastructure.Services
{
    /// <summary>
    /// Servicio de integración con MercadoPago para pagos en persona
    /// </summary>
    public class MercadoPagoService : IMercadoPagoService
    {
        private readonly HttpClient _httpClient;
        private readonly MercadoPagoSettings _settings;

        public MercadoPagoService(MercadoPagoSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            if (!_settings.IsValid())
            {
                throw new ArgumentException("La configuración de MercadoPago no es válida", nameof(settings));
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_settings.BaseUrl),
                Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds)
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.AccessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <inheritdoc/>
        public async Task<PaymentResult> ProcessPaymentAsync(Sale sale, string paymentMethod, string? terminalId = null)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));
            if (string.IsNullOrEmpty(paymentMethod)) throw new ArgumentNullException(nameof(paymentMethod));

            if (paymentMethod != "mercadopago")
            {
                return new PaymentResult
                {
                    Success = false,
                    ErrorMessage = $"Método de pago no soportado: {paymentMethod}",
                    Amount = (decimal)sale.Total
                };
            }

            var terminal = terminalId ?? _settings.TerminalId;
            if (string.IsNullOrEmpty(terminal))
            {
                return new PaymentResult
                {
                    Success = false,
                    ErrorMessage = "No se especificó un terminal de pago",
                    Amount = (decimal)sale.Total
                };
            }

            try
            {
                // Crear orden en MercadoPago
                var order = CreateOrderFromSale(sale);
                var orderResult = await CreateOrderAsync(order, terminal);

                if (!orderResult.Success)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        ErrorMessage = orderResult.ErrorMessage,
                        Amount = (decimal)sale.Total,
                        PaymentMethod = paymentMethod
                    };
                }

                // Esperar a que se complete el pago (esto es simplificado)
                // En producción, se debería usar webhooks o polling
                var status = await WaitForPaymentCompletion(orderResult.OrderId!, TimeSpan.FromMinutes(5));

                return new PaymentResult
                {
                    Success = status.Status == "paid",
                    TransactionId = orderResult.OrderId,
                    Amount = (decimal)sale.Total,
                    PaymentMethod = paymentMethod,
                    ErrorMessage = status.Status != "paid" ? $"Pago {status.Status}" : null
                };
            }
            catch (Exception ex)
            {
                return new PaymentResult
                {
                    Success = false,
                    ErrorMessage = $"Error procesando pago con MercadoPago: {ex.Message}",
                    Amount = (decimal)sale.Total,
                    PaymentMethod = paymentMethod
                };
            }
        }

        /// <inheritdoc/>
        public async Task<MercadoPagoOrderResult> CreateOrderAsync(MercadoPagoOrder order, string terminalId)
        {
            try
            {
                var requestBody = new
                {
                    external_reference = order.ExternalReference,
                    title = order.Title,
                    description = order.Description,
                    total_amount = order.Amount,
                    currency_id = order.Currency,
                    items = order.Items?.Select(item => new
                    {
                        title = item.Title,
                        description = item.Description,
                        quantity = item.Quantity,
                        unit_price = item.UnitPrice,
                        total_amount = item.TotalAmount
                    }).ToArray(),
                    point_of_interaction = new
                    {
                        type = "TERMINAL",
                        transaction_data = new
                        {
                            terminal_id = terminalId
                        }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/instore/orders", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new MercadoPagoOrderResult
                    {
                        Success = false,
                        ErrorMessage = $"Error API MercadoPago: {response.StatusCode} - {errorContent}"
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var orderResponse = JsonSerializer.Deserialize<MercadoPagoOrderResponse>(responseContent);

                return new MercadoPagoOrderResult
                {
                    Success = true,
                    OrderId = orderResponse?.Id,
                    QrCode = orderResponse?.QrCode,
                    QrCodeBase64 = orderResponse?.QrCodeBase64
                };
            }
            catch (Exception ex)
            {
                return new MercadoPagoOrderResult
                {
                    Success = false,
                    ErrorMessage = $"Error creando orden: {ex.Message}"
                };
            }
        }

        /// <inheritdoc/>
        public async Task<MercadoPagoOrderStatus> GetOrderStatusAsync(string orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/instore/orders/{orderId}");

                if (!response.IsSuccessStatusCode)
                {
                    return new MercadoPagoOrderStatus
                    {
                        OrderId = orderId,
                        Status = "error"
                    };
                }

                var content = await response.Content.ReadAsStringAsync();
                var orderData = JsonSerializer.Deserialize<MercadoPagoOrderResponse>(content);

                return new MercadoPagoOrderStatus
                {
                    OrderId = orderData?.Id,
                    Status = orderData?.Status,
                    PaidAmount = orderData?.TotalPaidAmount,
                    PaymentMethod = orderData?.PaymentMethod,
                    PaidDate = orderData?.PaidDate,
                    ExternalReference = orderData?.ExternalReference
                };
            }
            catch (Exception ex)
            {
                return new MercadoPagoOrderStatus
                {
                    OrderId = orderId,
                    Status = $"error: {ex.Message}"
                };
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CancelOrderAsync(string orderId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/instore/orders/{orderId}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private MercadoPagoOrder CreateOrderFromSale(Sale sale)
        {
            return new MercadoPagoOrder
            {
                ExternalReference = sale.Id.ToString(),
                Title = $"Venta #{sale.Id}",
                Description = $"Venta realizada el {sale.Date:dd/MM/yyyy}",
                Amount = (decimal)sale.Total,
                Currency = _settings.DefaultCurrency,
                Items = new List<MercadoPagoItem>
                {
                    new MercadoPagoItem
                    {
                        Title = "Productos",
                        Description = "Venta de productos",
                        Quantity = 1,
                        UnitPrice = (decimal)sale.Total,
                        TotalAmount = (decimal)sale.Total
                    }
                }
            };
        }

        private async Task<MercadoPagoOrderStatus> WaitForPaymentCompletion(string orderId, TimeSpan timeout)
        {
            var startTime = DateTime.Now;

            while (DateTime.Now - startTime < timeout)
            {
                var status = await GetOrderStatusAsync(orderId);

                if (status.Status == "paid" || status.Status == "cancelled" || status.Status == "expired")
                {
                    return status;
                }

                await Task.Delay(2000); // Esperar 2 segundos antes de consultar nuevamente
            }

            return new MercadoPagoOrderStatus
            {
                OrderId = orderId,
                Status = "timeout"
            };
        }

        /// <summary>
        /// Clase para deserializar respuesta de creación de orden
        /// </summary>
        private class MercadoPagoOrderResponse
        {
            public string? Id { get; set; }
            public string? Status { get; set; }
            public string? QrCode { get; set; }
            public string? QrCodeBase64 { get; set; }
            public decimal? TotalPaidAmount { get; set; }
            public string? PaymentMethod { get; set; }
            public DateTime? PaidDate { get; set; }
            public string? ExternalReference { get; set; }
        }
    }
}