using Aurora.Framework;
using Aurora.Platform.Domain.Applications.Models;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ComponentResponse : AuroraBaseResponse
    {
        public int ComponentId { get; private set; }

        public string Description { get; private set; }

        private ComponentResponse(bool isSuccess, string code, string message, ComponentData component)
            : base(isSuccess, code, message)
        {
            ComponentId = component != null ? component.ComponentId : -1;
            Description = component?.Description;
        }

        internal ComponentResponse(ComponentData component) : this(true, string.Empty, string.Empty, component) { }

        internal ComponentResponse(string code, string message) : this(false, code, message, null) { }
    }
}