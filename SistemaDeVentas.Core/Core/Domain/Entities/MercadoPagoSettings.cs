using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.Core.Domain.Entities
{
    /// <summary>
    /// Configuración para integración con MercadoPago
    /// </summary>
    public class MercadoPagoSettings
    {
        /// <summary>
        /// Access Token de MercadoPago
        /// </summary>
        [Required(ErrorMessage = "El Access Token es requerido")]
        public string? AccessToken { get; set; }

        /// <summary>
        /// Public Key de MercadoPago
        /// </summary>
        [Required(ErrorMessage = "La Public Key es requerida")]
        public string? PublicKey { get; set; }

        /// <summary>
        /// ID del punto de venta/terminal
        /// </summary>
        [Required(ErrorMessage = "El ID del terminal es requerido")]
        public string? TerminalId { get; set; }

        /// <summary>
        /// ID de la sucursal
        /// </summary>
        public string? BranchId { get; set; }

        /// <summary>
        /// Nombre de la sucursal
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// URL base de la API de MercadoPago
        /// </summary>
        public string BaseUrl { get; set; } = "https://api.mercadopago.com";

        /// <summary>
        /// Timeout para requests en segundos
        /// </summary>
        public int TimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Indica si está en modo sandbox/testing
        /// </summary>
        public bool IsSandbox { get; set; } = false;

        /// <summary>
        /// Moneda por defecto
        /// </summary>
        public string DefaultCurrency { get; set; } = "CLP";

        /// <summary>
        /// Categoría de la tienda
        /// </summary>
        public string? StoreCategory { get; set; }

        /// <summary>
        /// URL de notificación para webhooks
        /// </summary>
        public string? NotificationUrl { get; set; }

        /// <summary>
        /// Valida que la configuración sea correcta
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(AccessToken) &&
                   !string.IsNullOrEmpty(PublicKey) &&
                   !string.IsNullOrEmpty(TerminalId);
        }
    }
}