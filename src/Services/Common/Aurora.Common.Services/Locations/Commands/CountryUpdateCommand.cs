using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryUpdateCommand : IRequest<CountryResponse>
    {
        [Required]
        public short CountryId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string OfficialName { get; set; }

        public bool IsActive { get; set; }
    }
}