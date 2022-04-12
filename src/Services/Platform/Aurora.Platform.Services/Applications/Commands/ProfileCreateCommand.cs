using MediatR;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ProfileCreateCommand : IRequest<ProfileResponse>
    {
        public short ApplicationId { get; set; }

        public string Description { get; set; }
    }
}