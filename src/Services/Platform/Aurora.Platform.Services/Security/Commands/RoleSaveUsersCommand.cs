using MediatR;
using System.Collections.Generic;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleSaveUsersCommand : IRequest<RoleResponse>
    {
        public int RoleId { get; set; }
        public IList<int> UsersToAdd { get; set; }
        public IList<int> UsersToRemove { get; set; }
    }
}