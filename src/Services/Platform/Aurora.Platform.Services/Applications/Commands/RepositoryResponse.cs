using Aurora.Framework;
using Aurora.Platform.Domain.Applications.Models;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class RepositoryResponse : AuroraBaseResponse
    {
        public int RepositoryId { get; private set; }

        public string Code { get; private set; }

        public string Description { get; private set; }

        private RepositoryResponse(bool isSuccess, string code, string message, RepositoryData repository)
            : base(isSuccess, code, message)
        {
            RepositoryId = repository != null ? repository.RepositoryId : -1;
            Code = repository?.Code;
            Description = repository?.Description;
        }

        internal RepositoryResponse(RepositoryData repository) : this(true, string.Empty, string.Empty, repository) { }

        internal RepositoryResponse(string code, string message) : this(false, code, message, null) { }
    }
}