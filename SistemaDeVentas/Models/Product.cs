using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public bool? State { get; set; }

    public string? Name { get; set; }

    public int? Amount { get; set; }

    public double? SalePrice { get; set; }

    public int? Minimum { get; set; }

    public string? BarCode { get; set; }

    public byte[]? Photo { get; set; }

    public int? Stock { get; set; }

    public double? Price { get; set; }

    public bool? Exenta { get; set; }

    public Guid? IdTax { get; set; }

    public DateTime? Expiration { get; set; }

    public Guid? IdCategory { get; set; }

    public bool? IsPack { get; set; }

    public Guid? IdPack { get; set; }

    public string? Line { get; set; }

    public Guid? IdSubcategory { get; set; }
}
