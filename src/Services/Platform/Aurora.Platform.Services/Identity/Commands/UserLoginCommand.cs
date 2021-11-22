using MediatR;

namespace Aurora.Platform.Services.Identity.Commands
{
    public class UserLoginCommand : IRequest<IdentityAccess>
    {
        public string LoginName { get; set; }

        public string Password { get; set; }
    }
}