using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Applications.Repositories;

namespace Aurora.Platform.Repositories.Applications
{
    public class ComponentRepository : DataRepository<ComponentData>, IComponentRepository
    {
        #region Miembros privados de la clase

        private readonly PlatformDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public ComponentRepository(PlatformDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IComponentRepository

        /*async Task<ComponentData> IComponentRepository.GetAsync(short applicationId, string code)
        {
            return await _dataContext
                .Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ApplicationId.Equals(applicationId) && x.Code.Equals(code));
        }

        async Task<ComponentData> IComponentRepository.GetAsync(int componentId)
        {
            return await _dataContext
                .Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ComponentId.Equals(componentId));
        }

        async Task<IList<ComponentData>> IComponentRepository.GetListAsync(short applicationId)
        {
            return await _dataContext
                .Components
                .AsNoTracking()
                .Where(x => x.ApplicationId.Equals(applicationId))
                .ToListAsync();
        }*/

        #endregion
    }
}