namespace SistemaDeVentas.WinUI.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public User? User { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}