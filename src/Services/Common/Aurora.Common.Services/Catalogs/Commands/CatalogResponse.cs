using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Catalogs.Commands
{
    public class CatalogResponse : AuroraBaseResponse
    {
        public int CatalogId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        private CatalogResponse(bool isSuccess, string code, string message, CatalogData catalog)
            : base(isSuccess, code, message)
        {
            CatalogId = catalog != null ? catalog.CatalogId : -1;
            Code = catalog?.Code;
            Name = catalog?.Name;
        }

        internal CatalogResponse(CatalogData catalog) : this(true, string.Empty, string.Empty, catalog) { }

        internal CatalogResponse(string code, string message) : this(false, code, message, null) { }
    }
}