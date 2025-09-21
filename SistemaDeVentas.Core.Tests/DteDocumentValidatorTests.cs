using FluentValidation.TestHelper;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class DteDocumentValidatorTests
{
    private readonly DteDocumentValidator _validator;

    public DteDocumentValidatorTests()
    {
        _validator = new DteDocumentValidator();
    }

    [Fact]
    public void Should_Have_Error_When_IdDoc_Is_Null()
    {
        // Arrange
        var model = new DteDocument { IdDoc = null };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.IdDoc)
              .WithErrorMessage("El IdDoc es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_Emisor_Is_Null()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Emisor)
              .WithErrorMessage("El emisor es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_Receptor_Is_Null()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Receptor)
              .WithErrorMessage("El receptor es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_Totales_Is_Null()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Totales)
              .WithErrorMessage("Los totales son obligatorios.");
    }

    [Fact]
    public void Should_Have_Error_When_Detalles_Is_Null()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 1000m },
            Detalles = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Detalles)
              .WithErrorMessage("Los detalles son obligatorios.");
    }

    [Fact]
    public void Should_Have_Error_When_Detalles_Is_Empty()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 1000m },
            Detalles = new List<DetalleDte>()
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Detalles)
              .WithErrorMessage("Debe haber al menos un detalle.");
    }

    [Fact]
    public void Should_Have_Error_When_Detalles_LineNumbers_Are_Not_Unique()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 2000m },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto 1", MontoItem = 1000m },
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto 2", MontoItem = 1000m } // Duplicate line number
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Detalles)
              .WithErrorMessage("Los números de línea deben ser únicos, consecutivos y comenzar desde 1.");
    }

    [Fact]
    public void Should_Have_Error_When_Detalles_LineNumbers_Are_Not_Consecutive()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 2000m },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto 1", MontoItem = 1000m },
                new DetalleDte { NumeroLineaDetalle = 3, NombreItem = "Producto 2", MontoItem = 1000m } // Skip line 2
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Detalles)
              .WithErrorMessage("Los números de línea deben ser únicos, consecutivos y comenzar desde 1.");
    }

    [Fact]
    public void Should_Have_Error_When_Detalles_LineNumbers_Do_Not_Start_From_One()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 2000m },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 2, NombreItem = "Producto 1", MontoItem = 1000m },
                new DetalleDte { NumeroLineaDetalle = 3, NombreItem = "Producto 2", MontoItem = 1000m }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.Detalles)
              .WithErrorMessage("Los números de línea deben ser únicos, consecutivos y comenzar desde 1.");
    }

    [Fact]
    public void Should_Have_Error_When_TotalMonto_Does_Not_Match_Sum_Of_Detalles()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 1500m }, // Should be 2000
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto 1", MontoItem = 1000m },
                new DetalleDte { NumeroLineaDetalle = 2, NombreItem = "Producto 2", MontoItem = 1000m }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d)
              .WithErrorMessage("La suma de los montos de los detalles no coincide con el monto total.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_DteDocument()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 2000m },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto 1", MontoItem = 1000m },
                new DetalleDte { NumeroLineaDetalle = 2, NombreItem = "Producto 2", MontoItem = 1000m }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Validate_Emisor_When_Present()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 }, // Invalid RUT
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 1000m },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto", MontoItem = 1000m }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor("Emisor.RutEmisor")
              .WithErrorMessage("El RUT del emisor es obligatorio.");
    }

    [Fact]
    public void Should_Validate_Receptor_When_Present()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "", RazonSocialReceptor = "Cliente" }, // Invalid RUT
            Totales = new TotalesDte { MontoTotal = 1000m },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto", MontoItem = 1000m }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor("Receptor.RutReceptor")
              .WithErrorMessage("El RUT del receptor es obligatorio.");
    }

    [Fact]
    public void Should_Validate_Totales_When_Present()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = -1000m }, // Invalid negative total
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto", MontoItem = 1000m }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor("Totales.MontoTotal")
              .WithErrorMessage("El monto total debe ser mayor o igual a 0.");
    }

    [Fact]
    public void Should_Validate_Detalles_Items()
    {
        // Arrange
        var model = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 1000m },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "", MontoItem = 1000m } // Invalid empty name
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor("Detalles[0].NombreItem")
              .WithErrorMessage("El nombre del item es obligatorio.");
    }
}