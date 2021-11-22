using MediatR;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleUpdateDescriptionCommand : IRequest<RoleResponse>
    {
        public int RoleId { get; set; }

        public string Description { get; set; }
    }
}