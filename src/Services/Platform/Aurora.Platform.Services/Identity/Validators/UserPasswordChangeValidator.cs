using Aurora.Platform.Services.Identity.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Identity.Validators
{
    public class UserPasswordChangeValidator : AbstractValidator<UserPasswordChangeCommand>
    {
        public UserPasswordChangeValidator()
        {
            RuleFor(p => p.CurrentPassword)
                .NotEmpty().WithMessage("La contraseña actual del usuario es requerida.");

            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage("La nueva contraseña del usuario es requerida.")
                .MinimumLength(8).WithMessage("La longitud mínima de la contraseña es de 8 caracteres.")
                .MaximumLength(25).WithMessage("La longitud máxima de la contraseña es de 25 caracteres.");
        }
    }
}