using Aurora.Framework.Logic.Data;

namespace Aurora.Platform.Domain.Applications.Models
{
    public class ConnectionData : AuditableDataEntity
    {
        public int ConnectionId { get; set; }
        public int ProfileId { get; set; }
        public int ComponentId { get; set; }
        public string ConnString { get; set; }
        public bool IsEncrypted { get; set; }
        public ProfileData Profile { get; set; }
    }
}