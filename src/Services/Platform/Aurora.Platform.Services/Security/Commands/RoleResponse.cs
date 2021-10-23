using Aurora.Framework;
using Aurora.Platform.Domain.Security.Models;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleResponse : AuroraBaseResponse
    {
        public int RoleId { get; private set; }

        public string Name { get; private set; }

        private RoleResponse(bool isSuccess, string code, string message, RoleData role)
            : base(isSuccess, code, message)
        {
            RoleId = role != null ? role.RoleId : -1;
            Name = role?.Name;
        }

        internal RoleResponse(RoleData role) : this(true, string.Empty, string.Empty, role) { }

        internal RoleResponse(string code, string message) : this(false, code, message, null) { }
    }
}