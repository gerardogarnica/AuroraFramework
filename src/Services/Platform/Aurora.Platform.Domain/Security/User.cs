using System;
using System.Collections.Generic;

namespace Aurora.Platform.Domain.Security
{
    public class User
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName).Trim();
            }
        }
        public string Email { get; set; }
        public bool PasswordMustChange { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public bool IsDefaultUser { get; set; }
        public bool IsActive { get; set; }
        public IList<Role> Roles { get; set; }
    }
}