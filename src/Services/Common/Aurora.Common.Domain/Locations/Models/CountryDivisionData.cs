using Aurora.Framework.Logic.Data;
using System.Collections.Generic;

namespace Aurora.Common.Domain.Locations.Models
{
    public class CountryDivisionData : AuditableDataEntity
    {
        public short DivisionId { get; set; }
        public short CountryId { get; set; }
        public string Name { get; set; }
        public int LevelNumber { get; set; }
        public bool IsCityLevel { get; set; }
        public bool IsActive { get; set; }
        public CountryData Country { get; set; }
        public IList<LocationData> Locations { get; set; } = new List<LocationData>();
    }
}