using Aurora.Common.Domain.Locations.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryResponse : AuroraBaseResponse
    {
        public short CountryId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        private CountryResponse(bool isSuccess, string code, string message, CountryData country)
            : base(isSuccess, code, message)
        {
            CountryId = country != null ? country.CountryId : (short)-1;
            Code = country?.ThreeLettersCode;
            Name = country?.Name;
        }

        internal CountryResponse(CountryData country) : this(true, string.Empty, string.Empty, country) { }

        internal CountryResponse(string code, string message) : this(false, code, message, null) { }
    }
}