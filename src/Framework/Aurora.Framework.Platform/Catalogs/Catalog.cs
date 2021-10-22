using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Catalogs
{
    /// <summary>
    /// Representa una clase que contiene la información de un catálogo en Aurora Platform.
    /// </summary>
    public class Catalog
    {
        /// <summary>
        /// Identificador único del catálogo.
        /// </summary>
        public int CatalogId { get; set; }

        /// <summary>
        /// Código único del catálogo.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Nombre descriptivo del catálogo.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripción detallada del catálogo.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indica si el catálogo está visible para operaciones del lado cliente.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Indica si los datos del catálogo pueden ser modificados.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Lista de elementos del catálogo.
        /// </summary>
        public IList<CatalogItem> Items { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de un catálogo con sus elementos.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Código del catálogo.
            /// </summary>
            [Required(ErrorMessage = "El código del catálogo es obligatorio.")]
            [MinLength(3, ErrorMessage = "La longitud mínima del código del catálogo es 3 caracteres.")]
            [MaxLength(40, ErrorMessage = "La longitud máxima del código del catálogo es 40 caracteres.")]
            public string Code { get; set; }

            /// <summary>
            /// Nombre descriptivo del catálogo.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Descripción detallada del catálogo.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(200)]
            public string Description { get; set; }

            /// <summary>
            /// Indica si el catálogo estará disponible para la visualización en aplicaciones clientes.
            /// En caso de ser falso, este catálogo es de uso interno de una aplicación.
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// Indica si el catálogo puede ser modificado.
            /// </summary>
            public bool IsEditable { get; set; }

            /// <summary>
            /// Lista de elementos del catálogo.
            /// </summary>
            public IList<CatalogItem> Items { get; set; } = new List<CatalogItem>();

            /// <summary>
            /// Representa una clase que contiene la información para la creación de elemento de un catálogo.
            /// </summary>
            public class CatalogItem
            {
                /// <summary>
                /// Código del elemento de catálogo.
                /// </summary>
                [Required]
                [MinLength(3)]
                [MaxLength(40)]
                public string Code { get; set; }

                /// <summary>
                /// Descripción completa del elemento de catálogo.
                /// </summary>
                [Required]
                [MinLength(3)]
                [MaxLength(100)]
                public string Description { get; set; }

                /// <summary>
                /// Indica si el elemento de catálogo puede ser modificado.
                /// </summary>
                public bool IsEditable { get; set; }

                /// <summary>
                /// Indica si el elemento de catálogo se encuentra activo.
                /// </summary>
                public bool IsActive { get; set; }
            }
        }

        /// <summary>
        /// Representa una clase que contiene la información para la modificación de un catálogo.
        /// </summary>
        public class UpdateRequest
        {
            /// <summary>
            /// Código del catálogo.
            /// </summary>
            [Required(ErrorMessage = "El código del catálogo es obligatorio.")]
            public string Code { get; set; }

            /// <summary>
            /// Nombre descriptivo del catálogo.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Descripción detallada del catálogo.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string Description { get; set; }

            /// <summary>
            /// Indica si el catálogo estará disponible para la visualización en aplicaciones clientes.
            /// En caso de ser falso, este catálogo es de uso interno de una aplicación.
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// Indica si el catálogo puede ser modificado.
            /// </summary>
            public bool IsEditable { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de catálogos.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del catálogo.
            /// </summary>
            public int CatalogId { get; set; }

            /// <summary>
            /// Código único del catálogo.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// Nombre descriptivo del catálogo.
            /// </summary>
            public string Name { get; set; }
        }
    }
}