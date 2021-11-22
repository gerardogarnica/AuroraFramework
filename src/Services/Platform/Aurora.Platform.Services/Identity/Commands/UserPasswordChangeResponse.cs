namespace Aurora.Platform.Services.Identity.Commands
{
    public class UserPasswordChangeResponse
    {
        public bool IsSuccess { get; private set; }

        internal UserPasswordChangeResponse()
        {
            IsSuccess = true;
        }
    }
}