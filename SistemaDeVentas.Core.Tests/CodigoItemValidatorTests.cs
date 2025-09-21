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
    public void Should_Have_Error_When_TipoCodigo_Is_Null()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = null, ValorCodigo = "123" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.TipoCodigo)
              .WithErrorMessage("El tipo de código es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_TipoCodigo_Exceeds_MaxLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = new string('A', 11), ValorCodigo = "123" };

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
    public void Should_Have_Error_When_ValorCodigo_Is_Null()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = null };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ValorCodigo)
              .WithErrorMessage("El valor del código es obligatorio.");
    }

    [Fact]
    public void Should_Have_Error_When_ValorCodigo_Exceeds_MaxLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = new string('1', 36) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ValorCodigo)
              .WithErrorMessage("El valor del código no puede exceder los 35 caracteres.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_CodigoItem()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = "PROD001" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_TipoCodigo_Is_MaxLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = new string('A', 10), ValorCodigo = "123" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_ValorCodigo_Is_MaxLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = new string('1', 35) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_TipoCodigo_Is_MinLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "A", ValorCodigo = "123" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_ValorCodigo_Is_MinLength()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = "1" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_TipoCodigo_Has_Special_Characters()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "EAN13", ValorCodigo = "1234567890123" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_ValorCodigo_Has_Special_Characters()
    {
        // Arrange
        var model = new CodigoItem { TipoCodigo = "INT", ValorCodigo = "PROD-001_A" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}