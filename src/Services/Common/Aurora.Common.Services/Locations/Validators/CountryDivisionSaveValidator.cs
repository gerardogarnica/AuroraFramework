using Aurora.Common.Services.Locations.Commands;
using Aurora.Framework;
using FluentValidation;

namespace Aurora.Common.Services.Locations.Validators
{
    public class CountryDivisionSaveValidator : AbstractValidator<CountryDivisionSaveCommand>
    {
        public CountryDivisionSaveValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de división de país es requerido.")
                .MaximumLength(50).WithMessage("La longitud máxima del nombre de división de país es de 50 caracteres.");

            RuleFor(p => p.LevelNumber)
                .Must(LevelNumberValidRange).WithMessage("El número de nivel de división de país debe estar en el rango entre 1 y 5.");
        }

        private bool LevelNumberValidRange(int levelNumber)
        {
            return levelNumber.IsIntoInterval(1, 5);
        }
    }
}