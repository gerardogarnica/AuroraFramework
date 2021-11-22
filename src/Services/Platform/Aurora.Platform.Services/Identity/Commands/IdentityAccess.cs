namespace Aurora.Platform.Services.Identity.Commands
{
    public class IdentityAccess
    {
        public string AccessToken { get; private set; }

        internal IdentityAccess(string token)
        {
            AccessToken = token;
        }
    }
}