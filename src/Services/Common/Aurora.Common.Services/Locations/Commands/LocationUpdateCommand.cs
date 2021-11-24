using MediatR;

namespace Aurora.Common.Services.Locations.Commands
{
    public class LocationUpdateCommand : IRequest<LocationResponse>
    {
        public int LocationId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string AlternativeCode { get; set; }

        public bool IsActive { get; set; }
    }
}