using Aurora.Framework.Logic.Data;

namespace Aurora.Common.Domain.Catalogs.Models
{
    public class CatalogItemData : AuditableDataEntity
    {
        public int CatalogItemId { get; set; }
        public int CatalogId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public CatalogData Catalog { get; set; }
    }
}