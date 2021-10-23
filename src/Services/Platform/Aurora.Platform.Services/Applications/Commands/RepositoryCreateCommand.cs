using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class RepositoryCreateCommand : IRequest<RepositoryResponse>
    {
        [Required]
        public short ApplicationId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}