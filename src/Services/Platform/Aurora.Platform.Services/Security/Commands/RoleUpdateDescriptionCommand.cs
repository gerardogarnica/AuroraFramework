using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleUpdateDescriptionCommand : IRequest<RoleResponse>
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}