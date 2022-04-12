using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Exceptions;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ProfileResponse
    {
        public int ProfileId { get; private set; }

        public string Code { get; private set; }

        public string Description { get; private set; }

        internal ProfileResponse(ProfileData profile)
        {
            if (profile == null) throw new ProfileNullException();

            ProfileId = profile.ProfileId;
            Code = profile.Code;
            Description = profile.Description;
        }
    }
}