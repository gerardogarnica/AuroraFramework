using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Domain.Applications.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Queries
{
    public interface IRepositoryQueryService
    {
        Task<Repository> GetByCodeAsync(short applicationId, string code);
        Task<IList<Repository>> GetListAsync(short applicationId);
    }

    public class RepositoryQueryService : IRepositoryQueryService
    {
        #region Miembros privados de la clase

        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public RepositoryQueryService(
            IGenericRepository genericRepository,
            IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRepositoryQueryService

        async Task<Repository> IRepositoryQueryService.GetByCodeAsync(short applicationId, string code)
        {
            var repositoryData = await _genericRepository
                .GetAsync(applicationId, code);

            if (repositoryData == null) return null;

            return _mapper.Map<Repository>(repositoryData);
        }

        async Task<IList<Repository>> IRepositoryQueryService.GetListAsync(short applicationId)
        {
            var repositoriesData = await _genericRepository
                .GetListAsync(x => x.ApplicationId.Equals(applicationId), x => x.Description);

            return _mapper.Map<IList<Repository>>(repositoriesData);
        }

        #endregion
    }
}