using MediatR;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleCreateCommand : IRequest<RoleResponse>
    {
        public int RepositoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}