using FluentValidation.TestHelper;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class TotalesDteValidatorTests
{
    private readonly TotalesDteValidator _validator;

    public TotalesDteValidatorTests()
    {
        _validator = new TotalesDteValidator();
    }

    [Fact]
    public void Should_Have_Error_When_MontoNeto_Is_Negative()
    {
        // Arrange
        var model = new TotalesDte { MontoNeto = -100m, MontoTotal = 1000m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t.MontoNeto)
              .WithErrorMessage("El monto neto debe ser mayor o igual a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_MontoExento_Is_Negative()
    {
        // Arrange
        var model = new TotalesDte { MontoExento = -100m, MontoTotal = 1000m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t.MontoExento)
              .WithErrorMessage("El monto exento debe ser mayor o igual a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_TasaIVA_Is_Negative()
    {
        // Arrange
        var model = new TotalesDte { TasaIVA = -5m, MontoTotal = 1000m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t.TasaIVA)
              .WithErrorMessage("La tasa IVA debe estar entre 0 y 100.");
    }

    [Fact]
    public void Should_Have_Error_When_TasaIVA_Exceeds_Maximum()
    {
        // Arrange
        var model = new TotalesDte { TasaIVA = 150m, MontoTotal = 1000m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t.TasaIVA)
              .WithErrorMessage("La tasa IVA debe estar entre 0 y 100.");
    }

    [Fact]
    public void Should_Have_Error_When_IVA_Is_Negative()
    {
        // Arrange
        var model = new TotalesDte { IVA = -100m, MontoTotal = 1000m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t.IVA)
              .WithErrorMessage("El IVA debe ser mayor o igual a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_MontoTotal_Is_Zero()
    {
        // Arrange
        var model = new TotalesDte { MontoTotal = 0m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t.MontoTotal)
              .WithErrorMessage("El monto total debe ser mayor a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_MontoTotal_Is_Negative()
    {
        // Arrange
        var model = new TotalesDte { MontoTotal = -100m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t.MontoTotal)
              .WithErrorMessage("El monto total debe ser mayor a 0.");
    }

    [Fact]
    public void Should_Have_Error_When_MontoTotal_Does_Not_Match_Sum()
    {
        // Arrange - MontoTotal should be 1000 + 0 + 190 = 1190
        var model = new TotalesDte
        {
            MontoNeto = 1000m,
            MontoExento = 0m,
            IVA = 190m,
            MontoTotal = 1200m // Incorrect total
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(t => t)
              .WithErrorMessage("El monto total no coincide con la suma de neto, exento e IVA.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_TotalesDte()
    {
        // Arrange
        var model = new TotalesDte
        {
            MontoNeto = 100000m,
            MontoExento = 0m,
            TasaIVA = 19m,
            IVA = 19000m,
            MontoTotal = 119000m
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Optional_Fields_Are_Null()
    {
        // Arrange
        var model = new TotalesDte
        {
            MontoNeto = null,
            MontoExento = null,
            TasaIVA = null,
            IVA = null,
            MontoTotal = 1000m
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_MontoTotal_Matches_Sum_With_All_Components()
    {
        // Arrange
        var model = new TotalesDte
        {
            MontoNeto = 80000m,
            MontoExento = 20000m,
            IVA = 15200m, // 80000 * 0.19
            MontoTotal = 115200m // 80000 + 20000 + 15200
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_MontoTotal_Matches_Sum_With_Zero_IVA()
    {
        // Arrange
        var model = new TotalesDte
        {
            MontoNeto = 0m,
            MontoExento = 100000m,
            IVA = 0m,
            MontoTotal = 100000m
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_TasaIVA_Is_Zero()
    {
        // Arrange
        var model = new TotalesDte { TasaIVA = 0m, MontoTotal = 1000m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_TasaIVA_Is_Maximum()
    {
        // Arrange
        var model = new TotalesDte { TasaIVA = 100m, MontoTotal = 1000m };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}