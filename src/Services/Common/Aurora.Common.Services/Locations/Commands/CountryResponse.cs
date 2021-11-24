using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryResponse
    {
        public short CountryId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        internal CountryResponse(CountryData country)
        {
            if (country == null) throw new CountryNullException();

            CountryId = country.CountryId;
            Code = country.ThreeLettersCode;
            Name = country.Name;
        }
    }
}