using FluentValidation;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Domain.Validators.DTE;

/// <summary>
/// Validador para la entidad TotalesDte.
/// </summary>
public class TotalesDteValidator : AbstractValidator<TotalesDte>
{
    public TotalesDteValidator()
    {
        RuleFor(totales => totales.MontoNeto)
            .GreaterThanOrEqualTo(0).When(totales => totales.MontoNeto.HasValue)
            .WithMessage("El monto neto debe ser mayor o igual a 0.");

        RuleFor(totales => totales.MontoExento)
            .GreaterThanOrEqualTo(0).When(totales => totales.MontoExento.HasValue)
            .WithMessage("El monto exento debe ser mayor o igual a 0.");

        RuleFor(totales => totales.TasaIVA)
            .InclusiveBetween(0, 100).When(totales => totales.TasaIVA.HasValue)
            .WithMessage("La tasa IVA debe estar entre 0 y 100.");

        RuleFor(totales => totales.IVA)
            .GreaterThanOrEqualTo(0).When(totales => totales.IVA.HasValue)
            .WithMessage("El IVA debe ser mayor o igual a 0.");

        RuleFor(totales => totales.MontoTotal)
            .GreaterThan(0).WithMessage("El monto total debe ser mayor a 0.");

        // Validación de negocio: el monto total debe ser consistente
        RuleFor(totales => totales)
            .Must(totales =>
            {
                var neto = totales.MontoNeto ?? 0;
                var exento = totales.MontoExento ?? 0;
                var iva = totales.IVA ?? 0;
                var totalCalculado = neto + exento + iva;

                return Math.Abs(totalCalculado - totales.MontoTotal) < 0.01m;
            })
            .WithMessage("El monto total no coincide con la suma de neto, exento e IVA.");
    }
}