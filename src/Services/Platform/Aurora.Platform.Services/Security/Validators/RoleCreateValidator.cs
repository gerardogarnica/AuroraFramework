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
        }
    }
}