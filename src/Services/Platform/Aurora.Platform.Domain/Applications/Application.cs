using System.Collections.Generic;

namespace Aurora.Platform.Domain.Applications
{
    public class Application
    {
        public short ApplicationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasCustomConfig { get; set; }
        public IList<Component> Components { get; set; }
        public IList<Profile> Profiles { get; set; }
    }
}