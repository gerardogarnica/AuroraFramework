using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleCreateCommand : IRequest<RoleResponse>
    {
        [Required]
        public int RepositoryId { get; set; }

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