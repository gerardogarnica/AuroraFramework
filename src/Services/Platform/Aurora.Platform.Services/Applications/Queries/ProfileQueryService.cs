using Aurora.Platform.Domain.Applications.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Queries
{
    public interface IProfileQueryService
    {
        Task<Domain.Applications.Profile> GetByCodeAsync(string applicationCode, string code);
        Task<IList<Domain.Applications.Profile>> GetListAsync(string applicationCode);
    }

    public class ProfileQueryService : IProfileQueryService
    {
        #region Miembros privados de la clase

        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public ProfileQueryService(
            IProfileRepository profileRepository,
            IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IProfileQueryService

        async Task<Domain.Applications.Profile> IProfileQueryService.GetByCodeAsync(string applicationCode, string code)
        {
            var profileData = await _profileRepository
                .GetAsync(applicationCode, code);

            if (profileData == null) return null;

            return _mapper.Map<Domain.Applications.Profile>(profileData);
        }

        async Task<IList<Domain.Applications.Profile>> IProfileQueryService.GetListAsync(string applicationCode)
        {
            var profilesData = await _profileRepository
                .GetListAsync(x => x.Application.Code.Equals(applicationCode), x => x.Description);

            return _mapper.Map<IList<Domain.Applications.Profile>>(profilesData);
        }

        #endregion
    }
}