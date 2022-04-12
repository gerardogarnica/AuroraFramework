using Aurora.Platform.Services.Applications.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Applications.Validators
{
    public class ProfileCreateValidator : AbstractValidator<ProfileCreateCommand>
    {
        public ProfileCreateValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción del perfil de configuración es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción del perfil de configuración es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción del perfil de configuración es de 100 caracteres.");
        }
    }
}