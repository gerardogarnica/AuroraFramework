using Aurora.Common.Services.Settings.Commands;
using FluentValidation;

namespace Aurora.Common.Services.Settings.Validators
{
    public class SettingCreateValidator : AbstractValidator<SettingCreateCommand>
    {
        public SettingCreateValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("El código único del atributo de parametrización es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del código único del atributo de parametrización es de 3 caracteres.")
                .MaximumLength(40).WithMessage("La longitud máxima del código único del atributo de parametrización es de 40 caracteres.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre del atributo de parametrización es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre del atributo de parametrización es de 3 caracteres.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre del atributo de parametrización es de 50 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción del atributo de parametrización es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción del atributo de parametrización es de 3 caracteres.")
                .MaximumLength(200).WithMessage("La longitud máxima de la descripción del atributo de parametrización es de 200 caracteres.");

            RuleFor(p => p.ScopeType)
                .NotEmpty().WithMessage("El tipo de ámbito o alcance del atributo de parametrización es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del tipo de ámbito o alcance del atributo de parametrización es de 3 caracteres.")
                .MaximumLength(20).WithMessage("La longitud máxima del tipo de ámbito o alcance del atributo de parametrización es de 20 caracteres.");
        }
    }
}