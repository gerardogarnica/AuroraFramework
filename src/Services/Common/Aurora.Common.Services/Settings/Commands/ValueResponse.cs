using Aurora.Common.Domain.Settings.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Settings.Commands
{
    public class ValueResponse : AuroraBaseResponse
    {
        public int AttributeId { get; set; }

        public int RelationshipId { get; set; }

        private ValueResponse(bool isSuccess, string code, string message, AttributeValueData value)
            : base(isSuccess, code, message)
        {
            AttributeId = value != null ? value.AttributeId : -1;
            RelationshipId = value != null ? value.RelationshipId : -1;
        }

        internal ValueResponse(AttributeValueData value) : this(true, string.Empty, string.Empty, value) { }

        internal ValueResponse(string code, string message) : this(false, code, message, null) { }
    }
}