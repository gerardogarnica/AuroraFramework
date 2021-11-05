using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Common.Services.Locations.Commands;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Locations.Handlers
{
    public class CountryCreateHandler : IRequestHandler<CountryCreateCommand, CountryResponse>
    {
        #region Miembros privados de la clase

        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public CountryCreateHandler(
            ICountryRepository countryRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<CountryResponse> IRequestHandler<CountryCreateCommand, CountryResponse>.Handle(
            CountryCreateCommand request, CancellationToken cancellationToken)
        {
            // Se verifica si el país ya se encuentra registrado
            await VerifyIfCountryDataExists(request.ThreeLettersCode.Trim());

            // Se verifica la cantidad de niveles tipo ciudad
            VerifyCityLevel(request.Divisions.ToList());

            var entry = CreateCountryData(request);
            entry = await _countryRepository.InsertAsync(entry);

            return new CountryResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private CountryData CreateCountryData(CountryCreateCommand request)
        {
            return _mapper.Map<CountryData>(request);
        }

        private async Task VerifyIfCountryDataExists(string code)
        {
            var countryData = await _countryRepository.GetAsync(code);

            if (countryData != null)
            {
                throw new ExistsCountryCodeException(code);
            }
        }

        private void VerifyCityLevel(List<CountryDivisionCreate> divisions)
        {
            if (divisions.FindAll(x => x.IsCityLevel).Count > 1) throw new DuplicatedCountryCityLevelException();
        }

        #endregion
    }
}