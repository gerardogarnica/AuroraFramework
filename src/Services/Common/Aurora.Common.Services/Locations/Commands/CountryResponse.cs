using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryResponse : AuroraBaseResponse
    {
        public short CountryId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        internal CountryResponse(CountryData country)
            : base()
        {
            if (country == null) throw new CountryNullException();

            CountryId = country.CountryId;
            Code = country.ThreeLettersCode;
            Name = country.Name;
        }
    }
}