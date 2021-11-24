using MediatR;

namespace Aurora.Common.Services.Locations.Commands
{
    public class LocationCreateCommand : IRequest<LocationResponse>
    {
        public short DivisionId { get; set; }

        public int ParentLocationId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string AlternativeCode { get; set; }

        public bool IsActive { get; set; }
    }
}