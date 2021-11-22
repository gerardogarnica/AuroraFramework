using Aurora.Platform.Services.Identity.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Identity.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginValidator()
        {
            RuleFor(p => p.LoginName)
                .NotEmpty().WithMessage("El nombre de inicio de sesión de usuario es requerido.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("La contraseña de usuario es requerida.");
        }
    }
}