using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogCreateCommand : IRequest<CatalogResponse>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Description { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEditable { get; set; }

        public IList<CatalogItemCreate> Items { get; set; } = new List<CatalogItemCreate>();
    }

    public class CatalogItemCreate
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsEditable { get; set; }

        public bool IsActive { get; set; }
    }
}