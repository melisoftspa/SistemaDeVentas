using FluentValidation;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using System.Text.RegularExpressions;

namespace SistemaDeVentas.Core.Domain.Validators.DTE;

/// <summary>
/// Validador para la entidad Emisor.
/// </summary>
public class EmisorValidator : AbstractValidator<Emisor>
{
    public EmisorValidator()
    {
        RuleFor(emisor => emisor.RutEmisor)
            .NotEmpty().WithMessage("El RUT del emisor es obligatorio.")
            .Must(ValidarRut).WithMessage("El RUT del emisor no es válido.");

        RuleFor(emisor => emisor.RazonSocial)
            .NotEmpty().WithMessage("La razón social del emisor es obligatoria.")
            .MaximumLength(100).WithMessage("La razón social no puede exceder los 100 caracteres.");

        RuleFor(emisor => emisor.GiroEmisor)
            .NotEmpty().WithMessage("El giro del emisor es obligatorio.")
            .MaximumLength(80).WithMessage("El giro no puede exceder los 80 caracteres.");

        RuleFor(emisor => emisor.ActividadEconomica)
            .GreaterThan(0).WithMessage("La actividad económica debe ser mayor a 0.");

        RuleFor(emisor => emisor.CodigoSucursalSII)
            .GreaterThanOrEqualTo(0).When(emisor => emisor.CodigoSucursalSII.HasValue)
            .WithMessage("El código de sucursal SII debe ser mayor o igual a 0.");

        RuleFor(emisor => emisor.DireccionOrigen)
            .MaximumLength(60).When(emisor => !string.IsNullOrEmpty(emisor.DireccionOrigen))
            .WithMessage("La dirección de origen no puede exceder los 60 caracteres.");

        RuleFor(emisor => emisor.ComunaOrigen)
            .MaximumLength(20).When(emisor => !string.IsNullOrEmpty(emisor.ComunaOrigen))
            .WithMessage("La comuna de origen no puede exceder los 20 caracteres.");

        RuleFor(emisor => emisor.CiudadOrigen)
            .MaximumLength(20).When(emisor => !string.IsNullOrEmpty(emisor.CiudadOrigen))
            .WithMessage("La ciudad de origen no puede exceder los 20 caracteres.");

        RuleFor(emisor => emisor.CorreoEmisor)
            .EmailAddress().When(emisor => !string.IsNullOrEmpty(emisor.CorreoEmisor))
            .WithMessage("El correo electrónico del emisor no tiene un formato válido.");

        RuleFor(emisor => emisor.Telefono)
            .MaximumLength(20).When(emisor => !string.IsNullOrEmpty(emisor.Telefono))
            .WithMessage("El teléfono no puede exceder los 20 caracteres.");
    }

    /// <summary>
    /// Valida que el RUT sea válido según el algoritmo chileno.
    /// </summary>
    /// <param name="rut">El RUT a validar.</param>
    /// <returns>True si el RUT es válido.</returns>
    private bool ValidarRut(string rut)
    {
        if (string.IsNullOrWhiteSpace(rut))
            return false;

        // Remover puntos y guión
        var rutLimpio = Regex.Replace(rut, @"[\.\-]", "");

        if (!Regex.IsMatch(rutLimpio, @"^\d+[0-9Kk]$"))
            return false;

        // Separar número y dígito verificador
        var numero = rutLimpio.Substring(0, rutLimpio.Length - 1);
        var dv = rutLimpio[^1].ToString().ToUpper();

        // Calcular dígito verificador
        var suma = 0;
        var multiplicador = 2;

        for (var i = numero.Length - 1; i >= 0; i--)
        {
            suma += int.Parse(numero[i].ToString()) * multiplicador;
            multiplicador = multiplicador == 7 ? 2 : multiplicador + 1;
        }

        var resto = suma % 11;
        var dvCalculado = (11 - resto).ToString();

        if (resto == 1)
            dvCalculado = "K";
        else if (resto == 0)
            dvCalculado = "0";

        return dv == dvCalculado;
    }
}