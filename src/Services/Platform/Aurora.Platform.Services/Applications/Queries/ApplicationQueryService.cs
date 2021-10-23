using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Domain.Applications.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Queries
{
    public interface IApplicationQueryService
    {
        Task<Application> GetByCodeAsync(string code);
        Task<IList<Application>> GetListAsync();
    }

    public class ApplicationQueryService : IApplicationQueryService
    {
        #region Miembros privados de la clase

        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public ApplicationQueryService(
            IApplicationRepository applicationRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IApplicationQueryService

        async Task<Application> IApplicationQueryService.GetByCodeAsync(string code)
        {
            var applicationData = await _applicationRepository.GetAsync(code);

            if (applicationData == null) return null;

            return _mapper.Map<Application>(applicationData);
        }

        async Task<IList<Application>> IApplicationQueryService.GetListAsync()
        {
            var applicationsData = await _applicationRepository.GetAllAsync();

            return _mapper.Map<IList<Application>>(applicationsData);
        }

        #endregion
    }
}