using Aurora.Platform.Services.Security.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Security.Validators
{
    public class RoleUpdateDescriptionValidator : AbstractValidator<RoleUpdateDescriptionCommand>
    {
        public RoleUpdateDescriptionValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de rol de usuario es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción de rol de usuario es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción de rol de usuario es de 100 caracteres.");
        }
    }
}