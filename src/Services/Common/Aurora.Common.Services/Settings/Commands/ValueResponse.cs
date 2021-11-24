using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Settings.Models;

namespace Aurora.Common.Services.Settings.Commands
{
    public class ValueResponse
    {
        public int AttributeId { get; set; }

        public int RelationshipId { get; set; }

        internal ValueResponse(AttributeValueData value)
        {
            if (value == null) throw new AttributeNullException();

            AttributeId = value.AttributeId;
            RelationshipId = value.RelationshipId;
        }
    }
}