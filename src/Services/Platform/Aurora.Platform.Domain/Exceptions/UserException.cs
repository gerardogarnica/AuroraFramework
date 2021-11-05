using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class UserException : BusinessException
    {
        protected const string UserNullMessage = "El registro de usuario no puede ser nulo.";
        protected const string ExistsUserNameMessage = "El nombre de usuario '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidUserNameMessage = "El nombre de usuario '{0}' no se encuentra registrado.";
        protected const string DuplicatedRoleMessage = "Se encuentran duplicados los roles a agregar y eliminar del usuario.";
        protected const string InvalidCredentialsMessage = "El nombre de usuario o contraseña no son válidos o son incorrectos.";
        protected const string InactiveUserMessage = "El usuario {0} no se encuentra activo.";
        protected const string PasswordExpiredMessage = "La contraseña del usuario ha expirado. Se debe cambiar la contraseña para iniciar sesión.";
        protected const string InvalidPasswordPatternMessage = "La contraseña de usuario no cumple con el patrón {0}.";

        public UserException(string errorType, string message)
            : base("UserException", errorType, message) { }
    }

    public class UserNullException : UserException
    {
        public UserNullException()
            : base("UserNullException", UserNullMessage) { }
    }

    public class ExistsUserNameException : UserException
    {
        public ExistsUserNameException(string loginName)
            : base("ExistsUserNameException", string.Format(ExistsUserNameMessage, loginName)) { }
    }

    public class InvalidUserNameException : UserException
    {
        public InvalidUserNameException(string loginName)
            : base("InvalidUserNameException", string.Format(InvalidUserNameMessage, loginName)) { }
    }

    public class DuplicatedRoleException : UserException
    {
        public DuplicatedRoleException()
            : base("DuplicatedRoleException", DuplicatedRoleMessage) { }
    }

    public class InvalidCredentialsException : UserException
    {
        public InvalidCredentialsException()
            : base("InvalidCredentialsException", InvalidCredentialsMessage) { }
    }

    public class InactiveUserException : UserException
    {
        public InactiveUserException(string loginName)
            : base("InactiveUserException", string.Format(InactiveUserMessage, loginName)) { }
    }

    public class PasswordExpiredException : UserException
    {
        public PasswordExpiredException()
            : base("PasswordExpiredException", PasswordExpiredMessage) { }
    }

    public class InvalidPasswordPatternException : UserException
    {
        public InvalidPasswordPatternException(string patternDescription)
            : base("InvalidPasswordPatternException", string.Format(InvalidPasswordPatternMessage, patternDescription)) { }
    }
}