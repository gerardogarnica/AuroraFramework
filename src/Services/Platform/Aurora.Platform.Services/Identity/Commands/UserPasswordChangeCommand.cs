using MediatR;

namespace Aurora.Platform.Services.Identity.Commands
{
    public class UserPasswordChangeCommand : IRequest<UserPasswordChangeResponse>
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}