
namespace SistemaDeVentas.Models.SII
{
    public interface IResponse
    {
    }

    public class RestResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IResponse Response { get; set; }
    }
}
