
namespace SistemaDeVentas.Models.SII
{
    public class CheckStatusResponse : IResponse
    {
        public int? trackId { get; set; }
        public string? revisionEstado { get; set; }
        public string? revisionDetalle { get; set; }
    }
}
