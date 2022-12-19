
namespace SistemaDeVentas.Models.SII
{
    public class InvoiceResponse : IResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string PdfData { get; set; }
    }
}
