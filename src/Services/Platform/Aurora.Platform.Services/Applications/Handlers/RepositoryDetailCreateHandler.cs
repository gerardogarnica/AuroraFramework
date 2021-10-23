using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Services.Applications.Commands;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Handlers
{
    public class RepositoryDetailCreateHandler : IRequestHandler<RepositoryDetailCreateCommand, RepositoryResponse>
    {
        #region Miembros privados de la clase

        private readonly IComponentRepository _componentRepository;
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly IAuroraSession _auroraSession;
        private readonly AuroraSessionInfo _sessionInfo;

        #endregion

        #region Constructores de la clase

        public RepositoryDetailCreateHandler(
            IComponentRepository componentRepository,
            IGenericRepository genericRepository,
            IMapper mapper,
            IAuroraSession auroraSession)
        {
            _componentRepository = componentRepository;
            _genericRepository = genericRepository;
            _mapper = mapper;
            _auroraSession = auroraSession;

            _sessionInfo = _auroraSession.GetSessionInfo();
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<RepositoryResponse> IRequestHandler<RepositoryDetailCreateCommand, RepositoryResponse>.Handle(
            RepositoryDetailCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se obtiene el repositorio existente
                var entry = await GetExistentRepositoryData(request.RepositoryId);

                // Se verifica que el componente se encuentre registrado
                await VerifyIfComponentExists(request.ComponentId);

                // Se almacena el registro de detalle de repositorio
                SaveRepositoryDetailData(entry, request);
                entry = await _genericRepository.UpdateAsync(entry);

                return new RepositoryResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new RepositoryResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Métodos privados de la clase

        private void SaveRepositoryDetailData(
            RepositoryData repositoryData, RepositoryDetailCreateCommand request)
        {
            if (ExistsRepositoryDetailData(repositoryData, request.ComponentId))
            {
                UpdateRepositoryDetailData(repositoryData, request);
            }
            else
            {
                CreateRepositoryDetailData(repositoryData, request);
            }
        }

        private void CreateRepositoryDetailData(
            RepositoryData repositoryData, RepositoryDetailCreateCommand request)
        {
            repositoryData.Details.Add(_mapper.Map<RepositoryDetailData>(request));
        }

        private void UpdateRepositoryDetailData(
            RepositoryData repositoryData, RepositoryDetailCreateCommand request)
        {
            var detailData = repositoryData
                .Details
                .FirstOrDefault(x => x.RepositoryId.Equals(repositoryData.RepositoryId) && x.ComponentId.Equals(request.ComponentId));

            detailData.StringData = request.GetConnectionString();
            detailData.AddLastUpdated(_sessionInfo.UserLoginName);
        }

        private async Task<RepositoryData> GetExistentRepositoryData(int repositoryId)
        {
            var repositoryData = await _genericRepository.GetAsync(repositoryId);

            if (repositoryData == null)
            {
                throw new InvalidRepositoryIdException(repositoryId);
            }

            return repositoryData;
        }

        private async Task VerifyIfComponentExists(int componentId)
        {
            var componentData = await _componentRepository.GetAsync(x => x.ComponentId.Equals(componentId));

            if (componentData == null)
            {
                throw new InvalidComponentIdException(componentId);
            }
        }

        private bool ExistsRepositoryDetailData(RepositoryData repositoryData, int componentId)
        {
            return repositoryData
                .Details
                .ToList()
                .Exists(x => x.RepositoryId.Equals(repositoryData.RepositoryId) && x.ComponentId.Equals(componentId));
        }

        #endregion
    }
}