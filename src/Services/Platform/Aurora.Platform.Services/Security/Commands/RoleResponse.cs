using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleResponse
    {
        public int RoleId { get; private set; }

        public string Name { get; private set; }

        internal RoleResponse(RoleData role)
        {
            if (role == null) throw new RoleNullException();

            RoleId = role.RoleId;
            Name = role.Name;
        }
    }
}