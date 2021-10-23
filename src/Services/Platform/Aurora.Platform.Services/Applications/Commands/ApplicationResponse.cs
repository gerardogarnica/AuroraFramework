using Aurora.Framework;
using Aurora.Platform.Domain.Applications.Models;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ApplicationResponse : AuroraBaseResponse
    {
        public short ApplicationId { get; private set; }

        public string Code { get; private set; }

        public string Name { get; private set; }

        private ApplicationResponse(bool isSuccess, string code, string message, ApplicationData application)
            : base(isSuccess, code, message)
        {
            ApplicationId = application != null ? application.ApplicationId : (short)-1;
            Code = application?.Code;
            Name = application?.Name;
        }

        internal ApplicationResponse(ApplicationData application) : this(true, string.Empty, string.Empty, application) { }

        internal ApplicationResponse(string code, string message) : this(false, code, message, null) { }
    }
}