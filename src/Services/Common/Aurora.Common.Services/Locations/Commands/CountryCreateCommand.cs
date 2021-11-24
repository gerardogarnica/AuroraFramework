using MediatR;
using System.Collections.Generic;

namespace Aurora.Common.Services.Locations.Commands
{
    public class CountryCreateCommand : IRequest<CountryResponse>
    {
        public string Name { get; set; }

        public string OfficialName { get; set; }

        public string TwoLettersCode { get; set; }

        public string ThreeLettersCode { get; set; }

        public string ThreeDigitsCode { get; set; }

        public string InternetPrefix { get; set; }

        public bool IsActive { get; set; }

        public IList<CountryDivisionCreate> Divisions { get; set; } = new List<CountryDivisionCreate>();
    }

    public class CountryDivisionCreate
    {
        public string Name { get; set; }

        public int LevelNumber { get; set; }

        public bool IsCityLevel { get; set; }

        public bool IsActive { get; set; }
    }
}