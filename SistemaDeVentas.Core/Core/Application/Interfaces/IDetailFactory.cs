using System;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Core.Application.Interfaces
{
    public interface IDetailFactory
    {
        IDetail CreateDetail(string productName, double amount, double price, double tax);
    }
}