using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class ComponentException : BusinessException
    {
        public ComponentException(string message, string code)
            : base(message, "ComponentException", code) { }
    }

    public class ExistsComponentCodeException : ComponentException
    {
        public ExistsComponentCodeException(string code)
            : base(string.Format("El código de componente '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", code), "1001") { }
    }

    public class InvalidComponentIdException : ComponentException
    {
        public InvalidComponentIdException(int componentId)
            : base(string.Format("El ID de componente '{0}' no se encuentra registrado.", componentId), "1002") { }
    }
}