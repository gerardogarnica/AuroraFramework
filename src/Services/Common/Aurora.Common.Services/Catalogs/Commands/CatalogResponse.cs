using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Exceptions;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogResponse
    {
        public int CatalogId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        internal CatalogResponse(CatalogData catalog)
        {
            if (catalog == null) throw new CatalogNullException();

            CatalogId = catalog.CatalogId;
            Code = catalog.Code;
            Name = catalog.Name;
        }
    }
}