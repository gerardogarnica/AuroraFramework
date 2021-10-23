using Aurora.Framework.Logic.Data;
using System.Collections.Generic;

namespace Aurora.Common.Domain.Locations.Models
{
    public class CountryData : IDataEntity
    {
        public short CountryId { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string TwoLettersCode { get; set; }
        public string ThreeLettersCode { get; set; }
        public string ThreeDigitsCode { get; set; }
        public string InternetPrefix { get; set; }
        public bool IsActive { get; set; }
        public IList<CountryDivisionData> Divisions { get; set; } = new List<CountryDivisionData>();
    }
}