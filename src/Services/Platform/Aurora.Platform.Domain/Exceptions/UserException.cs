using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class UserException : BusinessException
    {
        public UserException(string message, string code)
            : base(message, "UserException", code) { }
    }

    public class ExistsUserNameException : UserException
    {
        public ExistsUserNameException(string loginName)
            : base(string.Format("El nombre de usuario '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", loginName), "1001") { }
    }

    public class InvalidUserNameException : UserException
    {
        public InvalidUserNameException(string loginName)
            : base(string.Format("El nombre de usuario '{0}' no se encuentra registrado.", loginName), "1002") { }
    }

    public class DuplicatedRoleException : UserException
    {
        public DuplicatedRoleException()
            : base(string.Format("Se encuentran duplicados los roles a agregar y eliminar del usuario."), "1003") { }
    }

    public class InvalidCredentialsException : UserException
    {
        public InvalidCredentialsException()
            : base(string.Format("El nombre de usuario o contraseña no son válidos o son incorrectos."), "1004") { }
    }

    public class InactiveUserException : UserException
    {
        public InactiveUserException(string loginName)
            : base(string.Format("El usuario {0} no se encuentra activo.", loginName), "1005") { }
    }

    public class PasswordExpiredException : UserException
    {
        public PasswordExpiredException()
            : base(string.Format("La contraseña del usuario ha expirado. Se debe cambiar la contraseña para iniciar sesión."), "1006") { }
    }

    public class InvalidUserPasswordPattern : UserException
    {
        public InvalidUserPasswordPattern(string patternDescription)
            : base(string.Format("La contraseña de usuario no cumple con el patrón {0}.", patternDescription), "1007") { }
    }
}