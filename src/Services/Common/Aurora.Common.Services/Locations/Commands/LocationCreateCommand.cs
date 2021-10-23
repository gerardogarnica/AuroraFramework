using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Locations.Commands
{
    public class LocationCreateCommand : IRequest<LocationResponse>
    {
        [Required]
        [Range(1, short.MaxValue)]
        public short DivisionId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ParentLocationId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Code { get; set; }

        public string AlternativeCode { get; set; }

        public bool IsActive { get; set; }
    }
}