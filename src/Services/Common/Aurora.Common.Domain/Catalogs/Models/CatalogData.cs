using Aurora.Framework.Logic.Data;
using System.Collections.Generic;

namespace Aurora.Common.Domain.Catalogs.Models
{
    public class CatalogData : IDataEntity
    {
        public int CatalogId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public IList<CatalogItemData> Items { get; set; } = new List<CatalogItemData>();
    }
}