using FluentValidation;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Domain.Validators.DTE;

/// <summary>
/// Validador para la entidad IdDoc.
/// </summary>
public class IdDocValidator : AbstractValidator<IdDoc>
{
    public IdDocValidator()
    {
        RuleFor(iddoc => iddoc.TipoDTE)
            .IsInEnum().WithMessage("El tipo de DTE no es válido.");

        RuleFor(iddoc => iddoc.Folio)
            .GreaterThan(0).WithMessage("El folio debe ser mayor a 0.")
            .LessThanOrEqualTo(99999999).WithMessage("El folio no puede exceder los 8 dígitos.");

        RuleFor(iddoc => iddoc.FechaEmision)
            .NotEmpty().WithMessage("La fecha de emisión es obligatoria.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de emisión no puede ser futura.");

        RuleFor(iddoc => iddoc.FechaVencimiento)
            .GreaterThanOrEqualTo(iddoc => iddoc.FechaEmision)
            .When(iddoc => iddoc.FechaVencimiento.HasValue)
            .WithMessage("La fecha de vencimiento debe ser posterior o igual a la fecha de emisión.");

        RuleFor(iddoc => iddoc.FormaPago)
            .InclusiveBetween(1, 3).When(iddoc => iddoc.FormaPago.HasValue)
            .WithMessage("La forma de pago debe ser 1 (contado), 2 (crédito) o 3 (sin costo financiero).");

        RuleFor(iddoc => iddoc.IndicadorTraslado)
            .InclusiveBetween(1, 9).When(iddoc => iddoc.IndicadorTraslado.HasValue)
            .WithMessage("El indicador de traslado debe estar entre 1 y 9.");
    }
}