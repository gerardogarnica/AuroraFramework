using MediatR;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryUpdateCommand : IRequest<CountryResponse>
    {
        public short CountryId { get; set; }

        public string Name { get; set; }

        public string OfficialName { get; set; }

        public bool IsActive { get; set; }
    }
}