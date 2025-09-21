using FluentValidation.TestHelper;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class DetalleDteValidatorTests
{
    private readonly DetalleDteValidator _validator;

    public DetalleDteValidatorTests()
    {
        _validator = new DetalleDteValidator();
    }

    [Fact]
    public void Should_Have_Error_When_NumeroLineaDetalle_Is_Zero()
    {
        // Arrange
        var model = new DetalleDte { NumeroLineaDetalle = 0, NombreItem = "Producto" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.NumeroLineaDetalle)
              .WithErrorMessage("El número de línea del detalle debe ser mayor a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_NombreItem_Is_Empty()
    {
        // Arrange
        var model = new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.NombreItem)
              .WithErrorMessage("El nombre del item es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_NombreItem_Exceeds_MaxLength()
    {
        // Arrange
        var model = new DetalleDte { NumeroLineaDetalle = 1, NombreItem = new string('A', 81) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.NombreItem)
              .WithErrorMessage("El nombre del item no puede exceder los 80 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_CantidadItem_Is_Negative()
    {
        // Arrange
        var model = new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto", CantidadItem = -1m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.CantidadItem)
              .WithErrorMessage("La cantidad del item debe ser mayor o igual a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_PrecioItem_Is_Negative()
    {
        // Arrange
        var model = new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto", PrecioItem = -100m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.PrecioItem)
              .WithErrorMessage("El precio del item debe ser mayor o igual a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_MontoItem_Is_Negative()
    {
        // Arrange
        var model = new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto", MontoItem = -100m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.MontoItem)
              .WithErrorMessage("El monto del item debe ser mayor o igual a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_IndicadorExencion_Is_Invalid()
    {
        // Arrange
        var model = new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Producto", IndicadorExencion = 3 };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d.IndicadorExencion)
              .WithErrorMessage("El indicador de exención debe ser 1 (afecto) o 2 (exento).");
    }

    [Fact]
    public void Should_Have_Error_When_Monto_Does_Not_Match_Cantidad_Times_Precio()
    {
        // Arrange
        var model = new DetalleDte
        {
            NumeroLineaDetalle = 1,
            NombreItem = "Producto",
            CantidadItem = 2m,
            PrecioItem = 100m,
            MontoItem = 150m // Should be 200
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(d => d)
              .WithErrorMessage("El monto del item no coincide con la cantidad multiplicada por el precio.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_DetalleDte()
    {
        // Arrange
        var model = new DetalleDte
        {
            NumeroLineaDetalle = 1,
            NombreItem = "Producto",
            CantidadItem = 2m,
            PrecioItem = 100m,
            MontoItem = 200m
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Validate_CodigoItem_When_Present()
    {
        // Arrange
        var model = new DetalleDte
        {
            NumeroLineaDetalle = 1,
            NombreItem = "Producto",
            CodigoItem = new CodigoItem { TipoCodigo = "", ValorCodigo = "123" } // Invalid TipoCodigo
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor("CodigoItem.TipoCodigo")
              .WithErrorMessage("El tipo de código es obligatorio.");
    }
}