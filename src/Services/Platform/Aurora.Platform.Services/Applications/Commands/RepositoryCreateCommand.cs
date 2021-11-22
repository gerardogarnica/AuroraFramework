using MediatR;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class RepositoryCreateCommand : IRequest<RepositoryResponse>
    {
        public short ApplicationId { get; set; }

        public string Description { get; set; }
    }
}