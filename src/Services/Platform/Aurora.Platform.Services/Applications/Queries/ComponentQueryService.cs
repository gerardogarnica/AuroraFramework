using Aurora.Platform.Domain.Applications;
using Aurora.Platform.Domain.Applications.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Applications.Queries
{
    public interface IComponentQueryService
    {
        Task<Component> GetByCodeAsync(string applicationCode, string code);
        Task<IList<Component>> GetListAsync(string applicationCode);
    }

    public class ComponentQueryService : IComponentQueryService
    {
        #region Miembros privados de la clase

        private readonly IComponentRepository _componentRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public ComponentQueryService(
            IComponentRepository componentRepository,
            IMapper mapper)
        {
            _componentRepository = componentRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IComponentQueryService

        async Task<Component> IComponentQueryService.GetByCodeAsync(string applicationCode, string code)
        {
            var componentData = await _componentRepository
                .GetAsync(x => x.Application.Code.Equals(applicationCode) && x.Code.Equals(code));

            if (componentData == null) return null;

            return _mapper.Map<Component>(componentData);
        }

        async Task<IList<Component>> IComponentQueryService.GetListAsync(string applicationCode)
        {
            var componentsData = await _componentRepository
                .GetListAsync(x => x.Application.Code.Equals(applicationCode), x => x.Code);

            return _mapper.Map<IList<Component>>(componentsData);
        }

        #endregion
    }
}