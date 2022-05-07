using Aurora.Platform.Services.Security.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Security.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateValidator()
        {
            RuleFor(p => p.LoginName)
                .NotEmpty().WithMessage("El nombre de inicio de sesión es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre de inicio de sesión es de 3 caracteres.")
                .MaximumLength(35).WithMessage("La longitud máxima del nombre de inicio de sesión es de 35 caracteres.");

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("El nombre del usuario es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre de usuario es de 3 caracteres.")
                .MaximumLength(40).WithMessage("La longitud máxima del nombre de usuario es de 40 caracteres.");

            RuleFor(p => p.LastName)
                .MaximumLength(40).WithMessage("La longitud máxima del apellido de usuario es de 40 caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El correo electrónico de usuario es requerida.")
                .MaximumLength(100).WithMessage("La longitud máxima del correo electrónico de usuario es de 100 caracteres.")
                .EmailAddress().WithMessage("La dirección de correo electrónico de usuario no cumple con el formato requerido.");
        }
    }
}