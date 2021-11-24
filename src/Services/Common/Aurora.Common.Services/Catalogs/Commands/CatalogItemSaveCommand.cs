using MediatR;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogItemSaveCommand : IRequest<CatalogResponse>
    {
        public string CatalogCode { get; set; }

        public string ItemCode { get; set; }

        public string Description { get; set; }

        public bool IsEditable { get; set; }

        public bool IsActive { get; set; }
    }
}