using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryDivisionSaveCommand : IRequest<CountryResponse>
    {
        [Required]
        public short CountryId { get; set; }

        [Required]
        public short DivisionId { get; set; }

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