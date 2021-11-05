using Aurora.Framework;

namespace Aurora.Platform.Services.Identity.Commands
{
    public class IdentityAccess : AuroraBaseResponse
    {
        public string AccessToken { get; private set; }

        internal IdentityAccess(string token)
            : base()
        {
            AccessToken = token;
        }
    }
}