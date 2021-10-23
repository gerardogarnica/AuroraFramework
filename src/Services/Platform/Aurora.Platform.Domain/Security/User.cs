using System;
using System.Collections.Generic;

namespace Aurora.Platform.Domain.Security
{
    public class User
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool PasswordMustChange { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public bool IsDefaultUser { get; set; }
        public bool IsActive { get; set; }
        public IList<Role> Roles { get; set; }
    }
}