using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class ApplicationException : BusinessException
    {
        protected const string ApplicationNullMessage = "El registro de aplicación no puede ser nulo.";
        protected const string ExistsApplicationCodeMessage = "El código de aplicación '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidApplicationIdMessage = "El ID de aplicación '{0}' no se encuentra registrado.";
        protected const string InvalidApplicationCodeMessage = "El código de aplicación '{0}' no se encuentra registrado.";

        public ApplicationException(string errorType, string message)
            : base("ApplicationException", errorType, message) { }
    }

    public class ApplicationNullException : ApplicationException
    {
        public ApplicationNullException()
            : base("ApplicationNullException", ApplicationNullMessage) { }
    }

    public class ExistsApplicationCodeException : ApplicationException
    {
        public ExistsApplicationCodeException(string code)
            : base("ExistsApplicationCodeException", string.Format(ExistsApplicationCodeMessage, code)) { }
    }

    public class InvalidApplicationIdException : ApplicationException
    {
        public InvalidApplicationIdException(short applicationId)
            : base("InvalidApplicationIdException", string.Format(InvalidApplicationIdMessage, applicationId)) { }
    }

    public class InvalidApplicationCodeException : ApplicationException
    {
        public InvalidApplicationCodeException(string code)
            : base("InvalidApplicationCodeException", string.Format(InvalidApplicationCodeMessage, code)) { }
    }
}