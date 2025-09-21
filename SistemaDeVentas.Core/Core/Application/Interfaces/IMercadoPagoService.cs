using System.Threading.Tasks;

namespace SistemaDeVentas.Core.Application.Interfaces
{
    /// <summary>
    /// Interfaz específica para integración con MercadoPago
    /// </summary>
    public interface IMercadoPagoService : IPaymentService
    {
        /// <summary>
        /// Crea una orden de pago en MercadoPago
        /// </summary>
        /// <param name="order">Datos de la orden</param>
        /// <param name="terminalId">ID del terminal de pago</param>
        /// <returns>Resultado de la creación de orden</returns>
        Task<MercadoPagoOrderResult> CreateOrderAsync(MercadoPagoOrder order, string terminalId);

        /// <summary>
        /// Consulta el estado de una orden
        /// </summary>
        /// <param name="orderId">ID de la orden</param>
        /// <returns>Estado de la orden</returns>
        Task<MercadoPagoOrderStatus> GetOrderStatusAsync(string orderId);

        /// <summary>
        /// Cancela una orden pendiente
        /// </summary>
        /// <param name="orderId">ID de la orden a cancelar</param>
        /// <returns>Resultado de la cancelación</returns>
        Task<bool> CancelOrderAsync(string orderId);
    }

    /// <summary>
    /// Datos de una orden de MercadoPago
    /// </summary>
    public class MercadoPagoOrder
    {
        public string? ExternalReference { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "CLP";
        public List<MercadoPagoItem>? Items { get; set; }
    }

    /// <summary>
    /// Item de una orden de MercadoPago
    /// </summary>
    public class MercadoPagoItem
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }

    /// <summary>
    /// Resultado de crear una orden en MercadoPago
    /// </summary>
    public class MercadoPagoOrderResult
    {
        public bool Success { get; set; }
        public string? OrderId { get; set; }
        public string? QrCode { get; set; }
        public string? QrCodeBase64 { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// Estado de una orden en MercadoPago
    /// </summary>
    public class MercadoPagoOrderStatus
    {
        public string? OrderId { get; set; }
        public string? Status { get; set; } // pending, paid, cancelled, expired
        public decimal? PaidAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? ExternalReference { get; set; }
    }
}