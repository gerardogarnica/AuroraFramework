using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Exceptions;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ApplicationResponse
    {
        public short ApplicationId { get; private set; }

        public string Code { get; private set; }

        public string Name { get; private set; }

        internal ApplicationResponse(ApplicationData application)
        {
            if (application == null) throw new ApplicationNullException();

            ApplicationId = application.ApplicationId;
            Code = application.Code;
            Name = application.Name;
        }
    }
}