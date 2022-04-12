namespace Aurora.Platform.Domain.Applications
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public short ApplicationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}