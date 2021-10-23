namespace Aurora.Platform.Domain.Security
{
    public class Role
    {
        public int RoleId { get; set; }
        public int RepositoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefaultRole { get; set; }
        public bool IsActive { get; set; }
    }
}