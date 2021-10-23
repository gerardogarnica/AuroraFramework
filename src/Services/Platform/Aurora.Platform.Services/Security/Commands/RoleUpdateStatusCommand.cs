using MediatR;

namespace Aurora.Platform.Services.Security.Commands
{
    public class RoleUpdateStatusCommand : IRequest<RoleResponse>
    {
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}