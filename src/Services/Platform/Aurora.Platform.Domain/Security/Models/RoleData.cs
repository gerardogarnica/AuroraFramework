using Aurora.Framework.Logic.Data;
using System.Collections.Generic;

namespace Aurora.Platform.Domain.Security.Models
{
    public class RoleData : AuditableDataEntity
    {
        public int RoleId { get; set; }
        public int RepositoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefaultRole { get; set; }
        public bool IsActive { get; set; }
        public IList<UserMembershipData> Memberships { get; set; } = new List<UserMembershipData>();
    }
}