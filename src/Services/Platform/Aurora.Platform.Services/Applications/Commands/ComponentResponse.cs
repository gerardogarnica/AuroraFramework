using Aurora.Platform.Domain.Applications.Models;
using Aurora.Platform.Domain.Exceptions;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class ComponentResponse
    {
        public int ComponentId { get; private set; }

        public string Description { get; private set; }

        internal ComponentResponse(ComponentData component)
        {
            if (component == null) throw new ComponentNullException();

            ComponentId = component.ComponentId;
            Description = component.Description;
        }
    }
}