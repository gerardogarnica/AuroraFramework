using MediatR;

namespace Aurora.Platform.Services.Security.Commands
{
    public class UserCreateCommand : IRequest<UserResponse>
    {
        public string LoginName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}