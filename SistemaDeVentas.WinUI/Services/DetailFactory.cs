using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.WinUI.Models;

namespace SistemaDeVentas.WinUI.Services
{
    public class DetailFactory : IDetailFactory
    {
        public IDetail CreateDetail(string productName, double amount, double price, double tax)
        {
            return new Detail
            {
                ProductName = productName,
                Amount = amount,
                Price = price,
                Tax = tax,
                Total = amount * price * (1 + tax / 100)
            };
        }
    }
}