using MediatR;
using System.Collections.Generic;

namespace Aurora.Platform.Services.Security.Commands
{
    public class UserSaveRolesCommand : IRequest<UserResponse>
    {
        public string LoginName { get; set; }
        public IList<int> RolesToAdd { get; set; }
        public IList<int> RolesToRemove { get; set; }
    }
}