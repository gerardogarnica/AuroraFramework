using Aurora.Framework.Logic.Data;
using System;
using System.Collections.Generic;

namespace Aurora.Platform.Domain.Applications.Models
{
    public class ApplicationData : IDataEntity
    {
        public short ApplicationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasCustomConfig { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<ComponentData> Components { get; set; } = new List<ComponentData>();
        public IList<ProfileData> Profiles { get; set; } = new List<ProfileData>();
    }
}