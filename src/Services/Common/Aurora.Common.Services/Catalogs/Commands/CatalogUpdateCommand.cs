using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogUpdateCommand : IRequest<CatalogResponse>
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEditable { get; set; }
    }
}