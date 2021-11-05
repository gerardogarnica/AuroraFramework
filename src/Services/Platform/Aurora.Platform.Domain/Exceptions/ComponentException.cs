using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class ComponentException : BusinessException
    {
        protected const string ComponentNullMessage = "El registro de componente no puede ser nulo.";
        protected const string ExistsComponentCodeMessage = "El código de componente '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidComponentIdMessage = "El ID de componente '{0}' no se encuentra registrado.";

        public ComponentException(string errorType, string message)
            : base("ComponentException", errorType, message) { }
    }

    public class ComponentNullException : ComponentException
    {
        public ComponentNullException()
            : base("ComponentNullException", ComponentNullMessage) { }
    }

    public class ExistsComponentCodeException : ComponentException
    {
        public ExistsComponentCodeException(string code)
            : base("ExistsComponentCodeException", string.Format(ExistsComponentCodeMessage, code)) { }
    }

    public class InvalidComponentIdException : ComponentException
    {
        public InvalidComponentIdException(int componentId)
            : base("InvalidComponentIdException", string.Format(InvalidComponentIdMessage, componentId)) { }
    }
}