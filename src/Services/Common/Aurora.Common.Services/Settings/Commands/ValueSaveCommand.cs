using Aurora.Framework.Settings;
using MediatR;

namespace Aurora.Common.Services.Settings.Commands
{
    public class ValueSaveCommand : AuroraAttributeValue, IRequest<ValueResponse>
    {
        public string Code { get; set; }

        public int RelationshipId { get; set; }
    }
}