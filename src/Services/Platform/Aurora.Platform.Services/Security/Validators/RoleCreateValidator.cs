using Aurora.Platform.Services.Security.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Security.Validators
{
    public class RoleCreateValidator : AbstractValidator<RoleCreateCommand>
    {
        public RoleCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de rol de usuario es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre de rol de usuario es de 3 caracteres.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de rol de usuario es de 50 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de rol de usuario es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción de rol de usuario es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción de rol de usuario es de 100 caracteres.");

            When(p => p.IsGlobal, () =>
            {
                RuleFor(p => p.ProfileId)
                    .Must(ProfileIsInvalid).WithMessage("El rol de usuario está configurado como global, no puede tener un perfil de configuración relacionado.");
            });

            When(p => !p.IsGlobal, () =>
            {
                RuleFor(p => p.ProfileId)
                    .Must(ProfileIsValid).WithMessage("El rol debe tener un perfil de configuración relacionado.");
            });
        }

        private bool ProfileIsInvalid(int profileId)
        {
            return profileId == 0;
        }

        private bool ProfileIsValid(int profileId)
        {
            return profileId > 0;
        }
    }
}