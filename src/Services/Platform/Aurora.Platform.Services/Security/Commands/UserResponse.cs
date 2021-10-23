using Aurora.Framework;
using Aurora.Platform.Domain.Security.Models;

namespace Aurora.Platform.Services.Security.Commands
{
    public class UserResponse : AuroraBaseResponse
    {
        public int UserId { get; private set; }

        public string LoginName { get; private set; }

        private UserResponse(bool isSuccess, string code, string message, UserData user)
            : base(isSuccess, code, message)
        {
            UserId = user != null ? user.UserId : -1;
            LoginName = user?.LoginName;
        }

        internal UserResponse(UserData user) : this(true, string.Empty, string.Empty, user) { }

        internal UserResponse(string code, string message) : this(false, code, message, null) { }
    }
}