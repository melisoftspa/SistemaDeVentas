using System.Threading.Tasks;
using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces
{
    /// <summary>
    /// Interfaz para servicios de pago
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Procesa un pago usando el método especificado
        /// </summary>
        /// <param name="sale">La venta a procesar</param>
        /// <param name="paymentMethod">Método de pago</param>
        /// <param name="terminalId">ID del terminal (opcional)</param>
        /// <returns>Resultado del pago</returns>
        Task<PaymentResult> ProcessPaymentAsync(Sale sale, string paymentMethod, string? terminalId = null);
    }

    /// <summary>
    /// Resultado de una operación de pago
    /// </summary>
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string? TransactionId { get; set; }
        public string? ErrorMessage { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}