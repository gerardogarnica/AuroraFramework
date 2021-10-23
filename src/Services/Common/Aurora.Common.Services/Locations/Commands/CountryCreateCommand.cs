using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryCreateCommand : IRequest<CountryResponse>
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string OfficialName { get; set; }

        [Required]
        [StringLength(2)]
        public string TwoLettersCode { get; set; }

        [Required]
        [StringLength(3)]
        public string ThreeLettersCode { get; set; }

        [Required]
        [StringLength(3)]
        public string ThreeDigitsCode { get; set; }

        [Required]
        [StringLength(3)]
        public string InternetPrefix { get; set; }

        public bool IsActive { get; set; }

        public IList<CountryDivisionCreate> Divisions { get; set; } = new List<CountryDivisionCreate>();
    }

    public class CountryDivisionCreate
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int LevelNumber { get; set; }

        public bool IsCityLevel { get; set; }

        public bool IsActive { get; set; }
    }
}