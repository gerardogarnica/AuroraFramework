using Aurora.Framework;
using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Exceptions;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class RepositoryResponse : AuroraBaseResponse
    {
        public int RepositoryId { get; private set; }

        public string Code { get; private set; }

        public string Description { get; private set; }

        internal RepositoryResponse(RepositoryData repository)
            : base()
        {
            if (repository == null) throw new RepositoryNullException();

            RepositoryId = repository.RepositoryId;
            Code = repository.Code;
            Description = repository.Description;
        }
    }
}