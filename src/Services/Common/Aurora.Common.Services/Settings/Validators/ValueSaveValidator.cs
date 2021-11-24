using Aurora.Common.Services.Settings.Commands;
using FluentValidation;

namespace Aurora.Common.Services.Settings.Validators
{
    public class ValueSaveValidator : AbstractValidator<ValueSaveCommand>
    {
        public ValueSaveValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("El código único del atributo de parametrización es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del código único del atributo de parametrización es de 3 caracteres.")
                .MaximumLength(40).WithMessage("La longitud máxima del código único del atributo de parametrización es de 40 caracteres.");
        }
    }
}