using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Catalogs
{
    /// <summary>
    /// Representa una clase que contiene la información de elementos de un catálogo en Aurora Platform.
    /// </summary>
    public class CatalogItem
    {
        /// <summary>
        /// Identificador único del elemento del catálogo.
        /// </summary>
        public int CatalogItemId { get; set; }

        /// <summary>
        /// Código único del elemento del catálogo.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Descripción detallada del elemento del catálogo.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indica si los datos del elemento de catálogo pueden ser modificados.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Indica si el elemento de catálogo se encuentra en estado activo.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación o modificación de un elemento de un catálogo.
        /// </summary>
        public class SaveRequest
        {
            /// <summary>
            /// Código del catálogo.
            /// </summary>
            [Required]
            public string CatalogCode { get; set; }

            /// <summary>
            /// Código del elemento del catálogo.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(40)]
            public string ItemCode { get; set; }

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
}