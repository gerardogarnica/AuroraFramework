using MediatR;

namespace Aurora.Platform.Services.Security.Commands
{
    public class UserUpdateCommand : IRequest<UserResponse>
    {
        public string LoginName { get; set; }
        public bool IsActive { get; set; }
    }
}