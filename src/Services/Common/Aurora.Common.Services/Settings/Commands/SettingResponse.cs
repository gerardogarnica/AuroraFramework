using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Framework;

namespace Aurora.Common.Services.Settings.Commands
{
    public class SettingResponse : AuroraBaseResponse
    {
        public int AttributeId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        internal SettingResponse(AttributeSettingData setting)
            : base()
        {
            if (setting == null) throw new AttributeNullException();

            AttributeId = setting.AttributeId;
            Code = setting.Code;
            Name = setting.Name;
        }
    }
}