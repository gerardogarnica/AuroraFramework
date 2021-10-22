using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Applications
{
    /// <summary>
    /// Representa una clase que contiene la información de un componente de aplicaciones en Aurora Platform.
    /// </summary>
    public class ComponentInfo
    {
        /// <summary>
        /// Identificador único del componente.
        /// </summary>
        public int ComponentId { get; set; }

        /// <summary>
        /// Identificador único de la aplicación.
        /// </summary>
        public short ApplicationId { get; set; }

        /// <summary>
        /// Código único del componente.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Descripción general del componente.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de un componente de aplicaciones en Aurora Platform.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Identificador único de la aplicación.
            /// </summary>
            [Required]
            public short ApplicationId { get; set; }

            /// <summary>
            /// Código único del componente.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(40)]
            public string Code { get; set; }

            /// <summary>
            /// Descripción general del componente.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string Description { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de componentes de aplicaciones de Aurora Platform.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del componente.
            /// </summary>
            public int ComponentId { get; private set; }

            /// <summary>
            /// Descripción general del componente.
            /// </summary>
            public string Description { get; private set; }
        }
    }
}