using MediatR;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryDivisionSaveCommand : IRequest<CountryResponse>
    {
        public short CountryId { get; set; }

        public short DivisionId { get; set; }

        public string Name { get; set; }

        public int LevelNumber { get; set; }

        public bool IsCityLevel { get; set; }

        public bool IsActive { get; set; }
    }
}