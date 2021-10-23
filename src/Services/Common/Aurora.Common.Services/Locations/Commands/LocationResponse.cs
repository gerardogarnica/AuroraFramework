using Aurora.Common.Domain.Locations.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Locations.Commands
{
    public class LocationResponse : AuroraBaseResponse
    {
        public int LocationId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        private LocationResponse(bool isSuccess, string code, string message, LocationData location)
            : base(isSuccess, code, message)
        {
            LocationId = location != null ? location.LocationId : -1;
            Name = location?.Name;
            Code = location?.Code;
        }

        internal LocationResponse(LocationData location) : this(true, string.Empty, string.Empty, location) { }

        internal LocationResponse(string code, string message) : this(false, code, message, null) { }
    }
}