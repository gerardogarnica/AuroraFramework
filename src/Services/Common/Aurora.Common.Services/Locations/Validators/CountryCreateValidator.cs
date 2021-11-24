using Aurora.Common.Services.Locations.Commands;
using FluentValidation;

namespace Aurora.Common.Services.Locations.Validators
{
    public class CountryCreateValidator : AbstractValidator<CountryCreateCommand>
    {
        public CountryCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de país es requerido.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de país es de 50 caracteres.");

            RuleFor(p => p.OfficialName)
                .NotEmpty().WithMessage("El nombre oficial de país es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre oficial de país es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima del nombre oficial de país es de 100 caracteres.");

            RuleFor(p => p.TwoLettersCode)
                .NotEmpty().WithMessage("El código de dos letras de país es requerido.")
                .Length(2).WithMessage("La longitud del código de dos letras de país es de 2 caracteres.")
                .Matches("^[a-zA-Z]+$").WithMessage("El código de dos letras de país solo debe contener letras.");

            RuleFor(p => p.ThreeLettersCode)
                .NotEmpty().WithMessage("El código de tres letras de país es requerido.")
                .Length(3).WithMessage("La longitud del código de tres letras de país es de 3 caracteres.")
                .Matches("^[a-zA-Z]+$").WithMessage("El código de tres letras de país solo debe contener letras.");

            RuleFor(p => p.ThreeDigitsCode)
                .NotEmpty().WithMessage("El código de tres dígitos de país es requerido.")
                .Length(3).WithMessage("La longitud del código de tres dígitos de país es de 3 caracteres.")
                .Matches("^[0-9]").WithMessage("El código de tres dígitos de país solo debe contener caracteres numéricos.");

            RuleFor(p => p.InternetPrefix)
                .NotEmpty().WithMessage("El prefijo de internet de país es requerido.")
                .Length(3).WithMessage("La longitud del prefijo de internet de país es de 3 caracteres.");
        }
    }
}