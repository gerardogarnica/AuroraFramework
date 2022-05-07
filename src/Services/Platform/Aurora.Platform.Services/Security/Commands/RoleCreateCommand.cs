using MediatR;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleCreateCommand : IRequest<RoleResponse>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsGlobal { get; set; }

        public int ProfileId { get; set; }
    }
}