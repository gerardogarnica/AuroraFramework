using Aurora.Framework.Exceptions;

namespace Aurora.Platform.Domain.Exceptions
{
    public class RepositoryException : BusinessException
    {
        protected const string RepositoryNullMessage = "El registro de repositorio no puede ser nulo.";
        protected const string ExistsRepositoryNameMessage = "La descripción del repositorio '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidRepositoryIdMessage = "El ID de repositorio '{0}' no se encuentra registrado.";
        protected const string InvalidSqlAuthenticationMessage = "La configuración de conexión de tipo SQL debe poseer un nombre de usuario y una contraseña válidas.";

        public RepositoryException(string errorType, string message)
            : base("RepositoryException", errorType, message) { }
    }

    public class RepositoryNullException : RepositoryException
    {
        public RepositoryNullException()
            : base("RepositoryNullException", RepositoryNullMessage) { }
    }

    public class ExistsRepositoryNameException : RepositoryException
    {
        public ExistsRepositoryNameException(string description)
            : base("ExistsRepositoryNameException", string.Format(ExistsRepositoryNameMessage, description)) { }
    }

    public class InvalidRepositoryIdException : RepositoryException
    {
        public InvalidRepositoryIdException(int repositoryId)
            : base("InvalidRepositoryIdException", string.Format(InvalidRepositoryIdMessage, repositoryId)) { }
    }

    public class InvalidSqlAuthenticationException : RepositoryException
    {
        public InvalidSqlAuthenticationException()
            : base("InvalidSqlAuthenticationException", InvalidSqlAuthenticationMessage) { }
    }
}