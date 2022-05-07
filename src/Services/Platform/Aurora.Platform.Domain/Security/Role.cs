namespace Aurora.Platform.Domain.Security
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public bool IsGlobal { get; set; }
        public int ProfileId { get; set; }
        public bool IsActive { get; set; }
    }
}