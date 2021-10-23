using System.Collections.Generic;

namespace Aurora.Common.Domain.Catalogs
{
    public class Catalog
    {
        public int CatalogId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public IList<CatalogItem> Items { get; set; }
    }
}