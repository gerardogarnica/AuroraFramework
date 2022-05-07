using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Services.Applications.Commands;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Handlers
{
    public class ComponentCreateHandler : IRequestHandler<ComponentCreateCommand, ComponentResponse>
    {
        #region Miembros privados de la clase

        private readonly IApplicationRepository _applicationRepository;
        private readonly IComponentRepository _componentRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public ComponentCreateHandler(
            IApplicationRepository applicationRepository,
            IComponentRepository componentRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _componentRepository = componentRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<ComponentResponse> IRequestHandler<ComponentCreateCommand, ComponentResponse>.Handle(
            ComponentCreateCommand request, CancellationToken cancellationToken)
        {
            // Se verifica que la aplicación se encuentre registrada
            await VerifyIfApplicationExists(request.ApplicationCode);

            // Se verifica si el componente ya se encuentra registrado
            await VerifyIfComponentExists(request.ApplicationCode, request.Code);

            // Se crea el registro de componente
            var entry = CreateComponentData(request);
            entry = await _componentRepository.InsertAsync(entry);

            return new ComponentResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private ComponentData CreateComponentData(ComponentCreateCommand request)
        {
            return _mapper.Map<ComponentData>(request);
        }

        private async Task VerifyIfApplicationExists(string applicationCode)
        {
            var applicationData = await _applicationRepository.GetAsync(applicationCode);

            if (applicationData == null)
            {
                throw new InvalidApplicationCodeException(applicationCode);
            }

            if (!applicationData.HasCustomConfig)
            {
                throw new CustomConfigNotAllowedException(applicationData.Name);
            }
        }

        private async Task VerifyIfComponentExists(string applicationCode, string code)
        {
            var componentData = await _componentRepository
                .GetAsync(x => x.Application.Code.Equals(applicationCode) && x.Code.Equals(code));

            if (componentData != null)
            {
                throw new ExistsComponentCodeException(code);
            }
        }

        #endregion
    }
}