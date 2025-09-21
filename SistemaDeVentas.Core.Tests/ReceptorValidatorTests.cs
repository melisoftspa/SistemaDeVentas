using FluentValidation.TestHelper;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class ReceptorValidatorTests
{
    private readonly ReceptorValidator _validator;

    public ReceptorValidatorTests()
    {
        _validator = new ReceptorValidator();
    }

    [Fact]
    public void Should_Have_Error_When_RutReceptor_Is_Invalid()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "12345678-0", RazonSocialReceptor = "Cliente" }; // Invalid RUT

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.RutReceptor)
              .WithErrorMessage("El RUT del receptor no es válido.");
    }

    [Fact]
    public void Should_Have_Error_When_RazonSocialReceptor_Is_Empty()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.RazonSocialReceptor)
              .WithErrorMessage("La razón social del receptor es obligatoria.");
    }

    [Fact]
    public void Should_Have_Error_When_RazonSocialReceptor_Exceeds_MaxLength()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = new string('A', 101) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.RazonSocialReceptor)
              .WithErrorMessage("La razón social no puede exceder los 100 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_GiroReceptor_Exceeds_MaxLength()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", GiroReceptor = new string('A', 41) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.GiroReceptor)
              .WithErrorMessage("El giro del receptor no puede exceder los 40 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_DireccionReceptor_Exceeds_MaxLength()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", DireccionReceptor = new string('A', 61) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.DireccionReceptor)
              .WithErrorMessage("La dirección del receptor no puede exceder los 60 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_ComunaReceptor_Exceeds_MaxLength()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", ComunaReceptor = new string('A', 21) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.ComunaReceptor)
              .WithErrorMessage("La comuna del receptor no puede exceder los 20 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_CiudadReceptor_Exceeds_MaxLength()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", CiudadReceptor = new string('A', 21) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.CiudadReceptor)
              .WithErrorMessage("La ciudad del receptor no puede exceder los 20 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_CorreoReceptor_Is_Invalid()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", CorreoReceptor = "invalid-email" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.CorreoReceptor)
              .WithErrorMessage("El correo electrónico del receptor no tiene un formato válido.");
    }

    [Fact]
    public void Should_Have_Error_When_Contacto_Exceeds_MaxLength()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", Contacto = new string('A', 81) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.Contacto)
              .WithErrorMessage("El contacto no puede exceder los 80 caracteres.");
    }

    [Fact]
    public void Should_Have_Error_When_CodigoInternoReceptor_Exceeds_MaxLength()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", CodigoInternoReceptor = new string('A', 21) };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(r => r.CodigoInternoReceptor)
              .WithErrorMessage("El código interno del receptor no puede exceder los 20 caracteres.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_RutReceptor_Is_Empty()
    {
        // Arrange - RUT vacío es permitido para receptor
        var model = new Receptor { RutReceptor = "", RazonSocialReceptor = "Cliente" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_RutReceptor_Is_Null()
    {
        // Arrange - RUT null es permitido para receptor
        var model = new Receptor { RutReceptor = null, RazonSocialReceptor = "Cliente" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_Receptor()
    {
        // Arrange
        var model = new Receptor
        {
            RutReceptor = "11111111-1",
            RazonSocialReceptor = "Cliente S.A.",
            GiroReceptor = "Venta de productos",
            DireccionReceptor = "Calle 123",
            ComunaReceptor = "Santiago",
            CiudadReceptor = "Santiago",
            CorreoReceptor = "cliente@example.com",
            Contacto = "Juan Pérez",
            CodigoInternoReceptor = "CLI001"
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Optional_Fields_Are_Null()
    {
        // Arrange
        var model = new Receptor
        {
            RutReceptor = "11111111-1",
            RazonSocialReceptor = "Cliente S.A.",
            GiroReceptor = null,
            DireccionReceptor = null,
            ComunaReceptor = null,
            CiudadReceptor = null,
            CorreoReceptor = null,
            Contacto = null,
            CodigoInternoReceptor = null
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Optional_Fields_Are_Empty()
    {
        // Arrange
        var model = new Receptor
        {
            RutReceptor = "11111111-1",
            RazonSocialReceptor = "Cliente S.A.",
            GiroReceptor = "",
            DireccionReceptor = "",
            ComunaReceptor = "",
            CiudadReceptor = "",
            CorreoReceptor = "",
            Contacto = "",
            CodigoInternoReceptor = ""
        };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_RutReceptor()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_Email()
    {
        // Arrange
        var model = new Receptor { RutReceptor = "11111111-1", RazonSocialReceptor = "Cliente", CorreoReceptor = "test@example.com" };

        // Act & Assert
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}