using Aurora.Framework.Logic.Data;
using System;
using System.Collections.Generic;

namespace Aurora.Platform.Domain.Security.Models
{
    public class UserCredentialData : AuditableDataEntity
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string PasswordControl { get; set; }
        public bool MustChange { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public UserData User { get; set; }
        public IList<UserCredentialLogData> CredentialLogs { get; set; } = new List<UserCredentialLogData>();
    }
}