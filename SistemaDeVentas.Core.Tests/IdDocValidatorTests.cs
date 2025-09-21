using FluentValidation.TestHelper;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class IdDocValidatorTests
{
    private readonly IdDocValidator _validator;

    public IdDocValidatorTests()
    {
        _validator = new IdDocValidator();
    }

    [Fact]
    public void Should_Have_Error_When_TipoDTE_Is_Invalid()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = (TipoDte)999, Folio = 1, FechaEmision = DateTime.Now };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.TipoDTE)
              .WithErrorMessage("El tipo de DTE no es válido.");
    }

    [Fact]
    public void Should_Have_Error_When_Folio_Is_Zero()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 0, FechaEmision = DateTime.Now };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.Folio)
              .WithErrorMessage("El folio debe ser mayor a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_Folio_Exceeds_MaxDigits()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 100000000, FechaEmision = DateTime.Now }; // 9 digits

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.Folio)
              .WithErrorMessage("El folio no puede exceder los 8 dígitos.");
    }

    [Fact]
    public void Should_Have_Error_When_FechaEmision_Is_Default()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = default };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.FechaEmision)
              .WithErrorMessage("La fecha de emisión es obligatoria.");
    }

    [Fact]
    public void Should_Have_Error_When_FechaEmision_Is_Future()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now.AddDays(1) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.FechaEmision)
              .WithErrorMessage("La fecha de emisión no puede ser futura.");
    }

    [Fact]
    public void Should_Have_Error_When_FechaVencimiento_Is_Before_FechaEmision()
    {
        // Arrange
        var fechaEmision = DateTime.Now;
        var model = new IdDoc
        {
            TipoDTE = TipoDte.FacturaAfecta,
            Folio = 1,
            FechaEmision = fechaEmision,
            FechaVencimiento = fechaEmision.AddDays(-1)
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.FechaVencimiento)
              .WithErrorMessage("La fecha de vencimiento debe ser posterior o igual a la fecha de emisión.");
    }

    [Fact]
    public void Should_Have_Error_When_FormaPago_Is_Invalid()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now, FormaPago = 4 };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.FormaPago)
              .WithErrorMessage("La forma de pago debe ser 1 (contado), 2 (crédito) o 3 (sin costo financiero).");
    }

    [Fact]
    public void Should_Have_Error_When_IndicadorTraslado_Is_Invalid()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now, IndicadorTraslado = 10 };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(i => i.IndicadorTraslado)
              .WithErrorMessage("El indicador de traslado debe estar entre 1 y 9.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_IdDoc()
    {
        // Arrange
        var model = new IdDoc
        {
            TipoDTE = TipoDte.FacturaAfecta,
            Folio = 12345,
            FechaEmision = DateTime.Now,
            FechaVencimiento = DateTime.Now.AddDays(30),
            FormaPago = 1,
            IndicadorTraslado = 1
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_FechaVencimiento_Is_Null()
    {
        // Arrange
        var model = new IdDoc
        {
            TipoDTE = TipoDte.FacturaAfecta,
            Folio = 12345,
            FechaEmision = DateTime.Now,
            FechaVencimiento = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_FormaPago_Is_Null()
    {
        // Arrange
        var model = new IdDoc
        {
            TipoDTE = TipoDte.FacturaAfecta,
            Folio = 12345,
            FechaEmision = DateTime.Now,
            FormaPago = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_IndicadorTraslado_Is_Null()
    {
        // Arrange
        var model = new IdDoc
        {
            TipoDTE = TipoDte.FacturaAfecta,
            Folio = 12345,
            FechaEmision = DateTime.Now,
            IndicadorTraslado = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_FechaVencimiento_Equals_FechaEmision()
    {
        // Arrange
        var fecha = DateTime.Now;
        var model = new IdDoc
        {
            TipoDTE = TipoDte.FacturaAfecta,
            Folio = 12345,
            FechaEmision = fecha,
            FechaVencimiento = fecha
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_FormaPago_Is_Valid_Value()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now, FormaPago = 2 };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_IndicadorTraslado_Is_Valid_Value()
    {
        // Arrange
        var model = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now, IndicadorTraslado = 5 };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}