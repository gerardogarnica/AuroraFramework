using Aurora.Framework.Logic.Data;
using System;
using System.Collections.Generic;

namespace Aurora.Platform.Domain.Applications.Models
{
    public class RepositoryData : IDataEntity
    {
        public int RepositoryId { get; set; }
        public short ApplicationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationData Application { get; set; }
        public IList<RepositoryDetailData> Details { get; set; } = new List<RepositoryDetailData>();
    }
}