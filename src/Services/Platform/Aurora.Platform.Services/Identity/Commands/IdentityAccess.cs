using Aurora.Framework;

namespace Aurora.Platform.Services.Identity.Commands
{
    public class IdentityAccess : AuroraBaseResponse
    {
        public string AccessToken { get; private set; }

        private IdentityAccess(bool isSuccess, string code, string message, string token)
            : base(isSuccess, code, message)
        {
            AccessToken = token;
        }

        internal IdentityAccess(string token) : this(true, string.Empty, string.Empty, token) { }

        internal IdentityAccess(string code, string message) : this(false, code, message, null) { }
    }
}