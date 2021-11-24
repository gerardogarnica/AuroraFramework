using MediatR;
using System.Collections.Generic;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogCreateCommand : IRequest<CatalogResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEditable { get; set; }

        public IList<CatalogItemCreate> Items { get; set; } = new List<CatalogItemCreate>();
    }

    public class CatalogItemCreate
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public bool IsEditable { get; set; }

        public bool IsActive { get; set; }
    }
}