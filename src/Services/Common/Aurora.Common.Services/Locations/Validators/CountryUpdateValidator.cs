using Aurora.Common.Services.Locations.Commands;
using FluentValidation;

namespace Aurora.Common.Services.Locations.Validators
{
    public class CountryUpdateValidator : AbstractValidator<CountryUpdateCommand>
    {
        public CountryUpdateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de país es requerido.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de país es de 50 caracteres.");

            RuleFor(p => p.OfficialName)
                .NotEmpty().WithMessage("El nombre oficial de país es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre oficial de país es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima del nombre oficial de país es de 100 caracteres.");
        }
    }
}