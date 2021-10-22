using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Applications
{
    /// <summary>
    /// Representa una clase que contiene la información de una aplicación en Aurora Platform.
    /// </summary>
    public class ApplicationInfo
    {
        /// <summary>
        /// Identificador único de la aplicación.
        /// </summary>
        public short ApplicationId { get; set; }

        /// <summary>
        /// Código único de la aplicación.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Nombre de la aplicación.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripción general de la aplicación.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lista de componentes de la aplicación.
        /// </summary>
        public IList<ComponentInfo> Components { get; set; }

        /// <summary>
        /// Lista de repositorios configurados para la aplicación.
        /// </summary>
        public IList<RepositoryInfo> Repositories { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de una aplicación en Aurora Platform.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Código único de la aplicación.
            /// </summary>
            [Required]
            [StringLength(36)]
            public string Code { get; set; }

            /// <summary>
            /// Nombre de la aplicación.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Descripción general de la aplicación.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string Description { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de aplicaciones de Aurora Platform.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único de la aplicación.
            /// </summary>
            public short ApplicationId { get; private set; }

            /// <summary>
            /// Código único de la aplicación.
            /// </summary>
            public string Code { get; private set; }

            /// <summary>
            /// Nombre de la aplicación.
            /// </summary>
            public string Name { get; private set; }
        }
    }
}