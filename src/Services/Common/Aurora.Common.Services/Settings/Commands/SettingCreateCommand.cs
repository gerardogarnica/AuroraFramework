using Aurora.Framework.Settings;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Common.Services.Settings.Commands
{
    public class SettingCreateCommand : AuroraAttributeSetting, IRequest<SettingResponse>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string ScopeType { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEditable { get; set; }

        public bool IsActive { get; set; }
    }
}