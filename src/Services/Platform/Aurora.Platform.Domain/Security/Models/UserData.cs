using Aurora.Framework.Logic.Data;
using System.Collections.Generic;

namespace Aurora.Platform.Domain.Security.Models
{
    public class UserData : AuditableDataEntity
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public UserCredentialData Credential { get; set; }
        public IList<UserMembershipData> Memberships { get; set; } = new List<UserMembershipData>();
    }
}