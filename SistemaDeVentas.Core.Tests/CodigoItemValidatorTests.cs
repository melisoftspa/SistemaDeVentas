using FluentValidation.TestHelper;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class CodigoItemValidatorTests
{
    private readonly CodigoItemValidator _validator;

    public CodigoItemValidatorTests()
    {
        _validator = new CodigoItemValidator();
    }

    [Fact]
    public void Should_Have_Error_When_TipoCodigo_Is_Empty()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "", ValorCodigo = "123" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.TipoCodigo)
              .WithErrorMessage("El tipo de código es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_TipoCodigo_Exceeds_MaxLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "ABCDEFGHIJK", ValorCodigo = "123" }; // 11 chars

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.TipoCodigo)
              .WithErrorMessage("El tipo de código no puede exceder los 10 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_ValorCodigo_Is_Empty()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = "" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ValorCodigo)
              .WithErrorMessage("El valor del código es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_ValorCodigo_Exceeds_MaxLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = new string('A', 36) }; // 36 chars

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ValorCodigo)
              .WithErrorMessage("El valor del código no puede exceder los 35 caracteres.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_CodigoItem()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = "12345" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}