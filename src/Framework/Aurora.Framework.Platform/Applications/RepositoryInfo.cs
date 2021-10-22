using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Applications
{
    /// <summary>
    /// Representa una clase que contiene la información de un repositorio de aplicaciones en Aurora Platform.
    /// </summary>
    public class RepositoryInfo
    {
        /// <summary>
        /// Identificador único del repositorio.
        /// </summary>
        public int RepositoryId { get; set; }

        /// <summary>
        /// Identificador único de la aplicación.
        /// </summary>
        public short ApplicationId { get; set; }

        /// <summary>
        /// Código único del repositorio.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Descripción general del repositorio.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de un repositorio de aplicaciones en Aurora Platform.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Identificador único de la aplicación.
            /// </summary>
            [Required]
            public short ApplicationId { get; set; }

            /// <summary>
            /// Descripción general del repositorio.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string Description { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de un detalle de un repositorio en Aurora Platform.
        /// </summary>
        public class DetailCreateRequest
        {
            /// <summary>
            /// Identificador único del repositorio.
            /// </summary>
            [Required]
            public int RepositoryId { get; set; }

            /// <summary>
            /// Identificador único del componente.
            /// </summary>
            [Required]
            public int ComponentId { get; set; }

            /// <summary>
            /// Nombre del servidor del repositorio.
            /// </summary>
            [Required]
            [MinLength(1)]
            [MaxLength(50)]
            public string ServerName { get; set; }

            /// <summary>
            /// Nombre de la base de datos del repositorio.
            /// </summary>
            [Required]
            [MinLength(2)]
            [MaxLength(30)]
            public string DatabaseName { get; set; }

            /// <summary>
            /// Tipo de autenticación.
            /// </summary>
            [Required]
            public SqlAuthenticationType AuthenticationType { get; set; }

            /// <summary>
            /// Nombre de usuario de inicio de sesión del repositorio, esta propiedad es opcional.
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// Contraseña de usuario de inicio de sesión del repositorio, esta propiedad es opcional.
            /// </summary>
            public string UserPassword { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de repositorios de aplicaciones de Aurora Platform.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del repositorio.
            /// </summary>
            public int RepositoryId { get; private set; }

            /// <summary>
            /// Código único del repositorio.
            /// </summary>
            public string Code { get; private set; }

            /// <summary>
            /// Descripción general del repositorio.
            /// </summary>
            public string Description { get; private set; }
        }
    }
}