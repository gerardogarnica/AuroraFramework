using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class RoleException : BusinessException
    {
        public RoleException(string message, string code)
            : base(message, "RoleException", code) { }
    }

    public class ExistsRoleNameException : RoleException
    {
        public ExistsRoleNameException(string name)
            : base(string.Format("El nombre del rol '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", name), "1001") { }
    }

    public class InvalidRoleIdException : RoleException
    {
        public InvalidRoleIdException(int roleId)
            : base(string.Format("El ID del rol '{0}' no se encuentra registrado.", roleId), "1002") { }
    }

    public class InvalidRoleNameException : RoleException
    {
        public InvalidRoleNameException(string name)
            : base(string.Format("El nombre de rol '{0}' no se encuentra registrado.", name), "1003") { }
    }
}