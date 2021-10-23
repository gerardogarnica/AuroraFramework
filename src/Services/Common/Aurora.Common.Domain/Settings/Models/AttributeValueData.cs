using Aurora.Framework.Logic.Data;

namespace Aurora.Common.Domain.Settings.Models
{
    public class AttributeValueData : AuditableDataEntity
    {
        public int AttributeId { get; set; }
        public int RelationshipId { get; set; }
        public string Value { get; set; }
        public AttributeSettingData AttributeSetting { get; set; }
    }
}