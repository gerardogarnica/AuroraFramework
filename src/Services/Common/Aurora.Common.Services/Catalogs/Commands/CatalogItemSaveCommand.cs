using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogItemSaveCommand : IRequest<CatalogResponse>
    {
        [Required]
        public string CatalogCode { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string ItemCode { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsEditable { get; set; }

        public bool IsActive { get; set; }
    }
}