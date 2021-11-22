using Aurora.Platform.Services.Applications.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Applications.Validators
{
    public class RepositoryCreateValidator : AbstractValidator<RepositoryCreateCommand>
    {
        public RepositoryCreateValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de repositorio es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción de repositorio es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción de repositorio es de 100 caracteres.");
        }
    }
}