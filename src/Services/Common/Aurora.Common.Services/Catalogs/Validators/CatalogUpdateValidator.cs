using Aurora.Common.Services.Catalogs.Commands;
using FluentValidation;

namespace Aurora.Common.Services.Catalogs.Validators
{
    public class CatalogUpdateValidator : AbstractValidator<CatalogUpdateCommand>
    {
        public CatalogUpdateValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("El código de catálogo es requerido.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de catálogo es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del nombre de catálogo es de 3 caracteres.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de catálogo es de 50 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de catálogo es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción de catálogo es de 3 caracteres.")
                .MaximumLength(200).WithMessage("La longitud máxima de la descripción de catálogo es de 200 caracteres.");
        }
    }
}