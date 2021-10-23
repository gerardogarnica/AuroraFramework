using System.Collections.Generic;

namespace Aurora.Common.Domain.Locations
{
    public class Country
    {
        public short CountryId { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string TwoLettersCode { get; set; }
        public string ThreeLettersCode { get; set; }
        public string ThreeDigitsCode { get; set; }
        public string InternetPrefix { get; set; }
        public bool IsActive { get; set; }
        public IList<CountryDivision> Divisions { get; set; }
        public IList<Location> FirstLevelLocations { get; set; }
    }
}