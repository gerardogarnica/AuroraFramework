namespace Aurora.Common.Domain.Catalogs
{
    public class CatalogItem
    {
        public int CatalogItemId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
    }
}