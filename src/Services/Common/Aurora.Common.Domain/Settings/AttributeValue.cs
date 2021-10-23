using Aurora.Framework;
using Aurora.Framework.Settings;

namespace Aurora.Common.Domain.Settings
{
    public class AttributeValue : AuroraAttributeValue
    {
        public int AttributeId { get; set; }
        public int RelationshipId { get; set; }
        public string Code { get; set; }
        public AuroraDataType DataType { get; set; }
    }
}