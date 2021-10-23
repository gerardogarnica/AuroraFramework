using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class ApplicationException : BusinessException
    {
        public ApplicationException(string message, string code)
            : base(message, "ApplicationException", code) { }
    }

    public class ExistsApplicationCodeException : ApplicationException
    {
        public ExistsApplicationCodeException(string code)
            : base(string.Format("El código de aplicación '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", code), "1001") { }
    }

    public class InvalidApplicationIdException : ApplicationException
    {
        public InvalidApplicationIdException(short applicationId)
            : base(string.Format("El ID de aplicación '{0}' no se encuentra registrado.", applicationId), "1002") { }
    }

    public class InvalidApplicationCodeException : ApplicationException
    {
        public InvalidApplicationCodeException(string code)
            : base(string.Format("El código de aplicación '{0}' no se encuentra registrado.", code), "1003") { }
    }
}