using FluentValidation;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Domain.Validators.DTE;

/// <summary>
/// Validador para la entidad CodigoItem.
/// </summary>
public class CodigoItemValidator : AbstractValidator<CodigoItem>
{
    public CodigoItemValidator()
    {
        RuleFor(codigo => codigo.TipoCodigo)
            .NotEmpty().WithMessage("El tipo de código es obligatorio.")
            .MaximumLength(10).WithMessage("El tipo de código no puede exceder los 10 caracteres.");

        RuleFor(codigo => codigo.ValorCodigo)
            .NotEmpty().WithMessage("El valor del código es obligatorio.")
            .MaximumLength(35).WithMessage("El valor del código no puede exceder los 35 caracteres.");
    }
}