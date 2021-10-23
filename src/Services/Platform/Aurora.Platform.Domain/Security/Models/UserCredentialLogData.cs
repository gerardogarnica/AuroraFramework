using Aurora.Framework.Logic.Data;
using System;

namespace Aurora.Platform.Domain.Security.Models
{
    public class UserCredentialLogData : IDataEntity
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public int ChangeNumber { get; set; }
        public string Password { get; set; }
        public string PasswordControl { get; set; }
        public bool MustChange { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public UserCredentialData Credential { get; set; }
    }
}