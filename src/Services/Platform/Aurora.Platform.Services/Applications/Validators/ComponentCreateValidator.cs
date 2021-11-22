using Aurora.Platform.Services.Applications.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Applications.Validators
{
    public class ComponentCreateValidator : AbstractValidator<ComponentCreateCommand>
    {
        public ComponentCreateValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("El código de componente es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del código de componente es de 3 caracteres.")
                .MaximumLength(40).WithMessage("La longitud máxima del código de componente es de 40 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de componente es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción de componente es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción de componente es de 100 caracteres.");
        }
    }
}