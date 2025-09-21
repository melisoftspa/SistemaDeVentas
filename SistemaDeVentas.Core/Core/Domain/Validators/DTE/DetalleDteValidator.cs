using FluentValidation;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Domain.Validators.DTE;

/// <summary>
/// Validador para la entidad DetalleDte.
/// </summary>
public class DetalleDteValidator : AbstractValidator<DetalleDte>
{
    public DetalleDteValidator()
    {
        RuleFor(detalle => detalle.NumeroLineaDetalle)
            .GreaterThan(0).WithMessage("El número de línea del detalle debe ser mayor a 0.");

        RuleFor(detalle => detalle.NombreItem)
            .NotEmpty().WithMessage("El nombre del item es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del item no puede exceder los 80 caracteres.");

        RuleFor(detalle => detalle.DescripcionItem)
            .MaximumLength(1000).When(detalle => !string.IsNullOrEmpty(detalle.DescripcionItem))
            .WithMessage("La descripción del item no puede exceder los 1000 caracteres.");

        RuleFor(detalle => detalle.CantidadItem)
            .GreaterThanOrEqualTo(0).When(detalle => detalle.CantidadItem.HasValue)
            .WithMessage("La cantidad del item debe ser mayor o igual a 0.");

        RuleFor(detalle => detalle.UnidadMedidaItem)
            .MaximumLength(4).When(detalle => !string.IsNullOrEmpty(detalle.UnidadMedidaItem))
            .WithMessage("La unidad de medida no puede exceder los 4 caracteres.");

        RuleFor(detalle => detalle.PrecioItem)
            .GreaterThanOrEqualTo(0).When(detalle => detalle.PrecioItem.HasValue)
            .WithMessage("El precio del item debe ser mayor o igual a 0.");

        RuleFor(detalle => detalle.MontoItem)
            .GreaterThanOrEqualTo(0).When(detalle => detalle.MontoItem.HasValue)
            .WithMessage("El monto del item debe ser mayor o igual a 0.");

        RuleFor(detalle => detalle.IndicadorExencion)
            .InclusiveBetween(1, 2).When(detalle => detalle.IndicadorExencion.HasValue)
            .WithMessage("El indicador de exención debe ser 1 (afecto) o 2 (exento).");

        // Validación de negocio: si hay cantidad y precio, el monto debe coincidir
        RuleFor(detalle => detalle)
            .Must(detalle => !detalle.CantidadItem.HasValue || !detalle.PrecioItem.HasValue ||
                           !detalle.MontoItem.HasValue ||
                           Math.Abs((decimal)(detalle.CantidadItem.Value * detalle.PrecioItem.Value) - detalle.MontoItem.Value) < 0.01m)
            .WithMessage("El monto del item no coincide con la cantidad multiplicada por el precio.");

        // Validación del código del item
        RuleFor(detalle => detalle.CodigoItem)
            .SetValidator(new CodigoItemValidator()).When(detalle => detalle.CodigoItem != null);
    }
}