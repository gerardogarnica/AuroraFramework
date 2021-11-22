using Aurora.Platform.Services.Applications.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Applications.Validators
{
    public class ApplicationCreateValidator : AbstractValidator<ApplicationCreateCommand>
    {
        public ApplicationCreateValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("El código de aplicación es requerido.")
                .Length(36).WithMessage("La longitud del código de aplicación es de 36 caracteres.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de aplicación es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre de aplicación es de 3 caracteres.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de aplicación es de 50 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de aplicación es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción de aplicación es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción de aplicación es de 100 caracteres.");
        }
    }
}