using Aurora.Platform.Services.Applications.Commands;
using FluentValidation;

namespace Aurora.Platform.Services.Applications.Validators
{
    public class RepositoryDetailCreateValidator : AbstractValidator<RepositoryDetailCreateCommand>
    {
        public RepositoryDetailCreateValidator()
        {
            RuleFor(p => p.ServerName)
                .NotEmpty().WithMessage("El nombre de servidor es requerido.")
                .MinimumLength(1).WithMessage("La longitud mínima del nombre de servidor es de 1 caracter.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de servidor es de 50 caracteres.");

            RuleFor(p => p.ServerName)
                .NotEmpty().WithMessage("El nombre de base de datos es requerido.")
                .MinimumLength(2).WithMessage("La longitud mínima del nombre de base de datos es de 2 caracteres.")
                .MaximumLength(30).WithMessage("La longitud máxima del nombre de base de datos es de 30 caracteres.");
        }
    }
}