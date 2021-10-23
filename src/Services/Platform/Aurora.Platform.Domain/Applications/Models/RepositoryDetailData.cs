using Aurora.Framework.Logic.Data;

namespace Aurora.Platform.Domain.Applications.Models
{
    public class RepositoryDetailData : AuditableDataEntity
    {
        public int RepositoryDetailId { get; set; }
        public int RepositoryId { get; set; }
        public int ComponentId { get; set; }
        public string StringData { get; set; }
        public RepositoryData Repository { get; set; }
    }
}