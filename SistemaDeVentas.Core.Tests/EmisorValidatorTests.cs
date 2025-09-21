using FluentValidation.TestHelper;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class EmisorValidatorTests
{
    private readonly EmisorValidator _validator;

    public EmisorValidatorTests()
    {
        _validator = new EmisorValidator();
    }

    [Fact]
    public void Should_Have_Error_When_RutEmisor_Is_Empty()
    {
        var model = new Emisor { RutEmisor = "", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(e => e.RutEmisor);
    }

    [Fact]
    public void Should_Have_Error_When_RutEmisor_Is_Invalid()
    {
        var model = new Emisor { RutEmisor = "12345678-0", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(e => e.RutEmisor);
    }

    [Fact]
    public void Should_Have_Error_When_RazonSocial_Is_Empty()
    {
        var model = new Emisor { RutEmisor = "11111111-1", RazonSocial = "", GiroEmisor = "Giro", ActividadEconomica = 620100 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(e => e.RazonSocial);
    }

    [Fact]
    public void Should_Have_Error_When_ActividadEconomica_Is_Zero()
    {
        var model = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(e => e.ActividadEconomica);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_Emisor()
    {
        var model = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}