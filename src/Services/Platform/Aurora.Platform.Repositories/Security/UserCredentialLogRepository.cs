using Aurora.Framework.Logic.Repositories;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;

namespace Aurora.Platform.Repositories.Security
{
    public class UserCredentialLogRepository : DataRepository<UserCredentialLogData>, IUserCredentialLogRepository
    {
        #region Miembros privados de la clase

        private readonly PlatformDataContext _dataContext;

        #endregion

        #region Constructores de la clase

        public UserCredentialLogRepository(PlatformDataContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implementación de la interface IUserCredentialLogRepository

        #endregion
    }
}