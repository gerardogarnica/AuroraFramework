using MediatR;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogUpdateCommand : IRequest<CatalogResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEditable { get; set; }
    }
}