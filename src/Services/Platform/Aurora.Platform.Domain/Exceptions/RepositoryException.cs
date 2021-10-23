using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class RepositoryException : BusinessException
    {
        public RepositoryException(string message, string code)
            : base(message, "RepositoryException", code) { }
    }

    public class ExistsRepositoryNameException : RepositoryException
    {
        public ExistsRepositoryNameException(string description)
            : base(string.Format("La descripción del repositorio '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", description), "1001") { }
    }

    public class InvalidRepositoryIdException : RepositoryException
    {
        public InvalidRepositoryIdException(int repositoryId)
            : base(string.Format("El ID de repositorio '{0}' no se encuentra registrado.", repositoryId), "1002") { }
    }

    public class InvalidSqlAuthenticationException : RepositoryException
    {
        public InvalidSqlAuthenticationException()
            : base(string.Format("La configuración de conexión de tipo SQL debe poseer un nombre de usuario y una contraseña válidas."), "1004") { }
    }
}