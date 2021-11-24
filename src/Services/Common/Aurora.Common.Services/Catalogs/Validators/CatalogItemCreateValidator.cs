using Aurora.Common.Services.Catalogs.Commands;
using FluentValidation;

namespace Aurora.Common.Services.Catalogs.Validators
{
    public class CatalogItemCreateValidator : AbstractValidator<CatalogItemCreate>
    {
        public CatalogItemCreateValidator()
        {
            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("El código de ítem de catálogo es requerido.")
                .MinimumLength(3).WithMessage("La longitud mínima del código de ítem de catálogo es de 3 caracteres.")
                .MaximumLength(40).WithMessage("La longitud máxima del código de ítem de catálogo es de 40 caracteres.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de catálogo es requerida.")
                .MinimumLength(3).WithMessage("La longitud mínima de la descripción de ítem de catálogo es de 3 caracteres.")
                .MaximumLength(100).WithMessage("La longitud máxima de la descripción de ítem de catálogo es de 100 caracteres.");
        }
    }
}