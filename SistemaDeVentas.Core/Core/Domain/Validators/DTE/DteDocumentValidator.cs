using FluentValidation;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Domain.Validators.DTE;

/// <summary>
/// Validador para la entidad DteDocument.
/// </summary>
public class DteDocumentValidator : AbstractValidator<DteDocument>
{
    public DteDocumentValidator()
    {
        RuleFor(dte => dte.IdDoc)
            .NotNull().WithMessage("El IdDoc es obligatorio.")
            .SetValidator(new IdDocValidator());

        RuleFor(dte => dte.Emisor)
            .NotNull().WithMessage("El emisor es obligatorio.")
            .SetValidator(new EmisorValidator());

        RuleFor(dte => dte.Receptor)
            .NotNull().WithMessage("El receptor es obligatorio.")
            .SetValidator(new ReceptorValidator());

        RuleFor(dte => dte.Totales)
            .NotNull().WithMessage("Los totales son obligatorios.")
            .SetValidator(new TotalesDteValidator());

        RuleFor(dte => dte.Detalles)
            .NotNull().WithMessage("Los detalles son obligatorios.")
            .NotEmpty().WithMessage("Debe haber al menos un detalle.")
            .ForEach(detalle => detalle.SetValidator(new DetalleDteValidator()));

        // Validación de negocio: números de línea únicos y consecutivos
        RuleFor(dte => dte.Detalles)
            .Must(detalles =>
            {
                var lineNumbers = detalles.Select(d => d.NumeroLineaDetalle).ToList();
                return lineNumbers.Distinct().Count() == lineNumbers.Count &&
                       lineNumbers.Min() == 1 &&
                       lineNumbers.Max() == detalles.Count;
            })
            .WithMessage("Los números de línea deben ser únicos, consecutivos y comenzar desde 1.");

        // Validación de negocio: consistencia entre detalles y totales
        RuleFor(dte => dte)
            .Must(dte =>
            {
                var totalMontoItems = dte.Detalles.Sum(d => d.MontoItem ?? 0);
                return Math.Abs(totalMontoItems - dte.Totales.MontoTotal) < 0.01m;
            })
            .WithMessage("La suma de los montos de los detalles no coincide con el monto total.");
    }
}