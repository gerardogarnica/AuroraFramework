using Aurora.Framework;
using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;

namespace Aurora.Platform.Services.Security.Commands
{
    public class UserResponse : AuroraBaseResponse
    {
        public int UserId { get; private set; }

        public string LoginName { get; private set; }

        internal UserResponse(UserData user)
            : base()
        {
            if (user == null) throw new RoleNullException();

            UserId = user.UserId;
            LoginName = user.LoginName;
        }
    }
}