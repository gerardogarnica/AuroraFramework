using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Common.Services.Locations.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Locations.Handlers
{
    public class CountryUpdateHandler : IRequestHandler<CountryUpdateCommand, CountryResponse>
    {
        #region Miembros privados de la clase

        private readonly ICountryRepository _countryRepository;

        #endregion

        #region Constructores de la clase

        public CountryUpdateHandler(
            ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<CountryResponse> IRequestHandler<CountryUpdateCommand, CountryResponse>.Handle(
            CountryUpdateCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene el país existente
            var entry = await GetExistentCountryData(request.CountryId);

            // Se actualiza el registro de país
            UpdateCountryData(entry, request);
            entry = await _countryRepository.UpdateAsync(entry);

            return new CountryResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private void UpdateCountryData(CountryData country, CountryUpdateCommand request)
        {
            country.Name = request.Name.Trim();
            country.OfficialName = request.OfficialName.Trim();
            country.IsActive = request.IsActive;
        }

        private async Task<CountryData> GetExistentCountryData(short countryId)
        {
            var countryData = await _countryRepository.GetAsync(countryId);

            if (countryData == null) throw new InvalidCountryIdException(countryId);

            return countryData;
        }

        #endregion
    }
}