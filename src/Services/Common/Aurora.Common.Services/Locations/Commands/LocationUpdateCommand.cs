using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Locations.Commands
{
    public class LocationUpdateCommand : IRequest<LocationResponse>
    {
        [Required]
        public int LocationId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Code { get; set; }

        public string AlternativeCode { get; set; }

        public bool IsActive { get; set; }
    }
}