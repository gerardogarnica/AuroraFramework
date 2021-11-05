using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Locations.Commands
{
    public class LocationResponse : AuroraBaseResponse
    {
        public int LocationId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        internal LocationResponse(LocationData location)
            : base()
        {
            if (location == null) throw new LocationNullException();

            LocationId = location.LocationId;
            Name = location.Name;
            Code = location.Code;
        }
    }
}