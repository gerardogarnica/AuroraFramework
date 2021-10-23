using Aurora.Framework;

namespace Aurora.Platform.Services.Identity.Commands
{
    public class UserPasswordChangeResponse : AuroraBaseResponse
    {
        private UserPasswordChangeResponse(bool isSuccess, string code, string message)
            : base(isSuccess, code, message) { }

        internal UserPasswordChangeResponse() : this(true, string.Empty, string.Empty) { }

        internal UserPasswordChangeResponse(string code, string message) : this(false, code, message) { }
    }
}