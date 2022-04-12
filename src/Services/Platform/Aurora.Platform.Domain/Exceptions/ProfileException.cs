using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class ProfileException : BusinessException
    {
        protected const string ProfileNullMessage = "El registro de perfil de configuración no puede ser nulo.";
        protected const string ExistsProfileNameMessage = "La descripción del perfil de configuración '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidProfileIdMessage = "El ID del perfil de configuración '{0}' no se encuentra registrado.";
        protected const string InvalidSqlAuthenticationMessage = "La configuración de conexión de tipo SQL debe poseer un nombre de usuario y una contraseña válidas.";

        public ProfileException(string message)
            : base("ProfileException", message) { }
    }

    public class ProfileNullException : ProfileException
    {
        public ProfileNullException()
            : base(ProfileNullMessage) { }
    }

    public class ExistsProfileNameException : ProfileException
    {
        public ExistsProfileNameException(string description)
            : base(string.Format(ExistsProfileNameMessage, description)) { }
    }

    public class InvalidProfileIdException : ProfileException
    {
        public InvalidProfileIdException(int repositoryId)
            : base(string.Format(InvalidProfileIdMessage, repositoryId)) { }
    }

    public class InvalidSqlAuthenticationException : ProfileException
    {
        public InvalidSqlAuthenticationException()
            : base(InvalidSqlAuthenticationMessage) { }
    }
}