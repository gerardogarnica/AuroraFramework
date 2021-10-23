using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Locations.Models;
using Aurora.Common.Domain.Locations.Repositories;
using Aurora.Common.Services.Locations.Commands;
using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Locations.Handlers
{
    public class CountryDivisionSaveHandler : IRequestHandler<CountryDivisionSaveCommand, CountryResponse>
    {
        #region Miembros privados de la clase

        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public CountryDivisionSaveHandler(
            ICountryRepository countryRepository,
            IMapper mapper,
            IAuroraSession auroraSession)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<CountryResponse> IRequestHandler<CountryDivisionSaveCommand, CountryResponse>.Handle(
            CountryDivisionSaveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se obtiene el país existente
                var entry = await GetExistentCountryData(request.CountryId);

                // Se almacena el registro de división de país
                SaveCountryDivisionData(entry, request);
                entry = await _countryRepository.UpdateAsync(entry);

                return new CountryResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new CountryResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Métodos privados de la clase

        private void SaveCountryDivisionData(CountryData countryData, CountryDivisionSaveCommand request)
        {
            if (ExistsCountryDivisionData(countryData, request.DivisionId, request.Name, request.LevelNumber, request.IsCityLevel))
            {
                UpdateCountryDivisionData(countryData, request);
            }
            else
            {
                CreateCountryDivisionData(countryData, request);
            }
        }

        private void CreateCountryDivisionData(CountryData countryData, CountryDivisionSaveCommand request)
        {
            countryData.Divisions.Add(_mapper.Map<CountryDivisionData>(request));
        }

        private void UpdateCountryDivisionData(CountryData countryData, CountryDivisionSaveCommand request)
        {
            var division = countryData
                .Divisions
                .FirstOrDefault(x => x.DivisionId.Equals(request.DivisionId));

            division.Name = request.Name.Trim();
            division.LevelNumber = request.LevelNumber;
            division.IsCityLevel = request.IsCityLevel;
            division.IsActive = request.IsActive;
            division.AddLastUpdated(_userName);
        }

        private async Task<CountryData> GetExistentCountryData(short countryId)
        {
            var countryData = await _countryRepository.GetAsync(countryId);

            if (countryData == null) throw new InvalidCountryIdException(countryId);

            return countryData;
        }

        private bool ExistsCountryDivisionData(
            CountryData countryData, short divisionId, string name, int levelNumber, bool isCityLevel)
        {
            var divisions = countryData.Divisions.ToList();
            var maxExpectedLevelNumber = divisions.Any() ? divisions.Max(x => x.LevelNumber) + 1 : 1;

            if (divisionId > 0)
            {
                if (!divisions.Exists(x => x.DivisionId.Equals(divisionId))) throw new InvalidCountryDivisionIdException(divisionId);

                if (divisions.Exists(x => !x.DivisionId.Equals(divisionId) && x.Name.Equals(name)))
                    throw new DuplicatedCountryDivisionException(countryData.Name, name);

                if (isCityLevel && divisions.Exists(x => !x.DivisionId.Equals(divisionId) && x.IsCityLevel.Equals(true)))
                    throw new DuplicatedCountryCityLevelException();

                if (levelNumber > maxExpectedLevelNumber)
                    throw new InvalidCountryDivisionLevelException(name, levelNumber, maxExpectedLevelNumber);

                return true;
            }

            if (divisions.Exists(x => x.Name.Equals(name))) throw new DuplicatedCountryDivisionException(countryData.Name, name);
            if (isCityLevel && divisions.Exists(x => x.IsCityLevel.Equals(true))) throw new DuplicatedCountryCityLevelException();
            if (levelNumber > maxExpectedLevelNumber) throw new InvalidCountryDivisionLevelException(name, levelNumber, maxExpectedLevelNumber);

            return false;
        }

        #endregion
    }
}