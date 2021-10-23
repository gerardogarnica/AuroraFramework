using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ComponentCreateCommand : IRequest<ComponentResponse>
    {
        [Required]
        public short ApplicationId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}