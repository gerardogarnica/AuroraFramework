using MediatR;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ApplicationCreateCommand : IRequest<ApplicationResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}