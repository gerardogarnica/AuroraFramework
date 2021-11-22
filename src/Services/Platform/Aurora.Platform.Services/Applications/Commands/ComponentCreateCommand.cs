using MediatR;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ComponentCreateCommand : IRequest<ComponentResponse>
    {
        public short ApplicationId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}