using Aurora.Framework;
using Aurora.Framework.Connections;
using Aurora.Framework.Cryptography;
using Aurora.Platform.Domain.Exceptions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Applications.Commands
{
    public class RepositoryDetailCreateCommand : IRequest<RepositoryResponse>
    {
        [Required]
        public int RepositoryId { get; set; }

        [Required]
        public int ComponentId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string ServerName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string DatabaseName { get; set; }

        [Required]
        public SqlAuthenticationType AuthenticationType { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string GetConnectionString()
        {
            // Validación de datos de conexión
            if (AuthenticationType.Equals(SqlAuthenticationType.SqlAuthentication))
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserPassword))
                {
                    throw new InvalidSqlAuthenticationException();
                }
            }
            else
            {
                UserName = null;
                UserPassword = null;
            }

            // Se genera la cadena de conexión
            var connectionString = AuthenticationType.Equals(SqlAuthenticationType.WindowsAuthentication)
                ? new SqlConnectionStringHelper(ServerName, DatabaseName)
                : new SqlConnectionStringHelper(ServerName, DatabaseName, UserName, UserPassword);

            var encryptedConnectionOne = EncryptionProvider.Protect(connectionString.ConnectionString);
            var encryptedConnectionTwo = EncryptionProvider.Protect(encryptedConnectionOne);

            return encryptedConnectionTwo;
        }
    }
}