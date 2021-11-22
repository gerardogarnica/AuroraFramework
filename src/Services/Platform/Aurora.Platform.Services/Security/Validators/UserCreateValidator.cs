using Aurora.Platform.Services.Security.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Security.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateValidator()
        {
            RuleFor(p => p.LoginName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre de usuario es de 3 caracteres.")
                .MaximumLength(35).WithMessage("La longitud máxima del nombre de usuario es de 35 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de usuario es requerida.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción de usuario es de 100 caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El correo electrónico de usuario es requerida.")
                .MaximumLength(100).WithMessage("La longitud máxima del correo electrónico de usuario es de 100 caracteres.")
                .EmailAddress().WithMessage("La dirección de correo electrónico de usuario no cumple con el formato requerido.");
        }
    }
}