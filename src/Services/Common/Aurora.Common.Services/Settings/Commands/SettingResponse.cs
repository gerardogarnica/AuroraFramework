using Aurora.Common.Domain.Settings.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Settings.Commands
{
    public class SettingResponse : AuroraBaseResponse
    {
        public int AttributeId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        private SettingResponse(bool isSuccess, string code, string message, AttributeSettingData setting)
            : base(isSuccess, code, message)
        {
            AttributeId = setting != null ? setting.AttributeId : -1;
            Code = setting?.Code;
            Name = setting?.Name;
        }

        internal SettingResponse(AttributeSettingData setting) : this(true, string.Empty, string.Empty, setting) { }

        internal SettingResponse(string code, string message) : this(false, code, message, null) { }
    }
}