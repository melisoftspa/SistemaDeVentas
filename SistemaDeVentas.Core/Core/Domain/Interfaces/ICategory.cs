using System;

namespace SistemaDeVentas.Core.Domain.Interfaces
{
    public interface ICategory
    {
        Guid Id { get; set; }
        string Text { get; set; }
        bool State { get; set; }
    }
}