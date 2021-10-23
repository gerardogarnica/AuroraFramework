using System.Collections.Generic;

namespace Aurora.Common.Domain.Locations
{
    public class Location
    {
        public int LocationId { get; set; }
        public short DivisionId { get; set; }
        public int ParentLocationId { get; set; }
        public string Code { get; set; }
        public string AlternativeCode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IList<Location> Locations { get; set; }
    }
}