using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class RoleException : BusinessException
    {
        protected const string RoleNullMessage = "El registro de rol de usuarios no puede ser nulo.";
        protected const string ExistsRoleNameMessage = "El nombre del rol '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidRoleIdMessage = "El ID del rol '{0}' no se encuentra registrado.";
        protected const string InvalidRoleNameMessage = "El nombre de rol '{0}' no se encuentra registrado.";

        public RoleException(string errorType, string message)
            : base("RoleException", errorType, message) { }
    }

    public class RoleNullException : RoleException
    {
        public RoleNullException()
            : base("RoleNullException", RoleNullMessage) { }
    }

    public class ExistsRoleNameException : RoleException
    {
        public ExistsRoleNameException(string name)
            : base("ExistsRoleNameException", string.Format(ExistsRoleNameMessage, name)) { }
    }

    public class InvalidRoleIdException : RoleException
    {
        public InvalidRoleIdException(int roleId)
            : base("InvalidRoleIdException", string.Format(InvalidRoleIdMessage, roleId)) { }
    }

    public class InvalidRoleNameException : RoleException
    {
        public InvalidRoleNameException(string name)
            : base("InvalidRoleNameException", string.Format(InvalidRoleNameMessage, name)) { }
    }
}