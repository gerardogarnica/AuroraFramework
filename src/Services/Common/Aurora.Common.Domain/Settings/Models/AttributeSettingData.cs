using Aurora.Framework.Logic.Data;
using System.Collections.Generic;

namespace Aurora.Common.Domain.Settings.Models
{
    public class AttributeSettingData : IDataEntity
    {
        public int AttributeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScopeType { get; set; }
        public string DataType { get; set; }
        public string Configuration { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public IList<AttributeValueData> Values { get; set; } = new List<AttributeValueData>();
    }
}