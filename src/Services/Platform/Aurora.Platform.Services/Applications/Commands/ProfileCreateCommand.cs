using MediatR;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ProfileCreateCommand : IRequest<ProfileResponse>
    {
        public string ApplicationCode { get; set; }

        public string Description { get; set; }
    }
}