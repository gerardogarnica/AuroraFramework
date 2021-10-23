using Aurora.Framework.Logic.Data;

namespace Aurora.Platform.Domain.Security.Models
{
    public class UserMembershipData : AuditableDataEntity
    {
        public int MembershipId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsDefaultMembership { get; set; }
        public bool IsActive { get; set; }
        public UserData User { get; set; }
        public RoleData Role { get; set; }
    }
}