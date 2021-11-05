using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Exceptions;
using Aurora.Framework;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogResponse : AuroraBaseResponse
    {
        public int CatalogId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        internal CatalogResponse(CatalogData catalog)
            : base()
        {
            if (catalog == null) throw new CatalogNullException();

            CatalogId = catalog.CatalogId;
            Code = catalog.Code;
            Name = catalog.Name;
        }
    }
}