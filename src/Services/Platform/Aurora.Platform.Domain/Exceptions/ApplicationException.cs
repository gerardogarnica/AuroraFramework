using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class ApplicationException : BusinessException
    {
        protected const string ApplicationNullMessage = "El registro de aplicación no puede ser nulo.";
        protected const string CustomConfigNotAllowedMessage = "La aplicación '{0}' no permite la creación de configuraciones personalizadas.";
        protected const string ExistsApplicationCodeMessage = "El código de aplicación '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidApplicationIdMessage = "El ID de aplicación '{0}' no se encuentra registrado.";
        protected const string InvalidApplicationCodeMessage = "El código de aplicación '{0}' no se encuentra registrado.";

        public ApplicationException(string message)
            : base("ApplicationException", message) { }
    }

    public class ApplicationNullException : ApplicationException
    {
        public ApplicationNullException()
            : base(ApplicationNullMessage) { }
    }

    public class CustomConfigNotAllowedException : ApplicationException
    {
        public CustomConfigNotAllowedException(string name)
            : base(string.Format(CustomConfigNotAllowedMessage, name)) { }
    }

    public class ExistsApplicationCodeException : ApplicationException
    {
        public ExistsApplicationCodeException(string code)
            : base(string.Format(ExistsApplicationCodeMessage, code)) { }
    }

    public class InvalidApplicationIdException : ApplicationException
    {
        public InvalidApplicationIdException(short applicationId)
            : base(string.Format(InvalidApplicationIdMessage, applicationId)) { }
    }

    public class InvalidApplicationCodeException : ApplicationException
    {
        public InvalidApplicationCodeException(string code)
            : base(string.Format(InvalidApplicationCodeMessage, code)) { }
    }
}