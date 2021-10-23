using Aurora.Framework.Logic.Data;
using System;

namespace Aurora.Platform.Domain.Applications.Models
{
    public class ComponentData : IDataEntity
    {
        public int ComponentId { get; set; }
        public short ApplicationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationData Application { get; set; }
    }
}