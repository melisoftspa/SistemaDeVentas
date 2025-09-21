using FluentValidation;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using System.Text.RegularExpressions;

namespace SistemaDeVentas.Core.Domain.Validators.DTE;

/// <summary>
/// Validador para la entidad Receptor.
/// </summary>
public class ReceptorValidator : AbstractValidator<Receptor>
{
    public ReceptorValidator()
    {
        // El RUT del receptor puede ser opcional para algunos tipos de documentos
        RuleFor(receptor => receptor.RutReceptor)
            .Must(rut => string.IsNullOrEmpty(rut) || ValidarRut(rut))
            .WithMessage("El RUT del receptor no es válido.");

        RuleFor(receptor => receptor.RazonSocialReceptor)
            .NotEmpty().WithMessage("La razón social del receptor es obligatoria.")
            .MaximumLength(100).WithMessage("La razón social no puede exceder los 100 caracteres.");

        RuleFor(receptor => receptor.GiroReceptor)
            .MaximumLength(40).When(receptor => !string.IsNullOrEmpty(receptor.GiroReceptor))
            .WithMessage("El giro del receptor no puede exceder los 40 caracteres.");

        RuleFor(receptor => receptor.DireccionReceptor)
            .MaximumLength(60).When(receptor => !string.IsNullOrEmpty(receptor.DireccionReceptor))
            .WithMessage("La dirección del receptor no puede exceder los 60 caracteres.");

        RuleFor(receptor => receptor.ComunaReceptor)
            .MaximumLength(20).When(receptor => !string.IsNullOrEmpty(receptor.ComunaReceptor))
            .WithMessage("La comuna del receptor no puede exceder los 20 caracteres.");

        RuleFor(receptor => receptor.CiudadReceptor)
            .MaximumLength(20).When(receptor => !string.IsNullOrEmpty(receptor.CiudadReceptor))
            .WithMessage("La ciudad del receptor no puede exceder los 20 caracteres.");

        RuleFor(receptor => receptor.CorreoReceptor)
            .EmailAddress().When(receptor => !string.IsNullOrEmpty(receptor.CorreoReceptor))
            .WithMessage("El correo electrónico del receptor no tiene un formato válido.");

        RuleFor(receptor => receptor.Contacto)
            .MaximumLength(80).When(receptor => !string.IsNullOrEmpty(receptor.Contacto))
            .WithMessage("El contacto no puede exceder los 80 caracteres.");

        RuleFor(receptor => receptor.CodigoInternoReceptor)
            .MaximumLength(20).When(receptor => !string.IsNullOrEmpty(receptor.CodigoInternoReceptor))
            .WithMessage("El código interno del receptor no puede exceder los 20 caracteres.");
    }

    /// <summary>
    /// Valida que el RUT sea válido según el algoritmo chileno.
    /// </summary>
    /// <param name="rut">El RUT a validar.</param>
    /// <returns>True si el RUT es válido o vacío.</returns>
    private bool ValidarRut(string rut)
    {
        if (string.IsNullOrWhiteSpace(rut))
            return true; // Permitir vacío para receptor

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