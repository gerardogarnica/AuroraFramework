using Aurora.Framework.Settings;
using MediatR;

namespace Aurora.Common.Services.Settings.Commands
{
    public class SettingCreateCommand : AuroraAttributeSetting, IRequest<SettingResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ScopeType { get; set; }

        public bool IsVisible { get; set; }

        public bool IsEditable { get; set; }

        public bool IsActive { get; set; }
    }
}