using Aurora.Framework.Logic.Data;

namespace Aurora.Common.Domain.Locations.Models
{
    public class LocationData : AuditableDataEntity
    {
        public int LocationId { get; set; }
        public short DivisionId { get; set; }
        public int ParentLocationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AlternativeCode { get; set; }
        public bool IsActive { get; set; }
        public CountryDivisionData Division { get; set; }
    }
}