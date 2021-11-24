using Aurora.Common.Services.Locations.Commands;
using FluentValidation;

namespace Aurora.Common.Services.Locations.Validators
{
    public class LocationCreateValidator : AbstractValidator<LocationCreateCommand>
    {
        public LocationCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de localidad es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre de localidad es de 3 caracteres.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de localidad es de 50 caracteres.");
        }
    }
}