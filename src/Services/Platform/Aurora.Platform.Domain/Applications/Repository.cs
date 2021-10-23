namespace Aurora.Platform.Domain.Applications
{
    public class Repository
    {
        public int RepositoryId { get; set; }
        public short ApplicationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}