using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ApplicationCreateCommand : IRequest<ApplicationResponse>
    {
        [Required]
        [StringLength(36)]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}