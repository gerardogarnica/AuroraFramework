using Aurora.Framework.Settings;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Settings.Commands
{
    public class ValueSaveCommand : AuroraAttributeValue, IRequest<ValueResponse>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Code { get; set; }

        public int RelationshipId { get; set; }
    }
}