using Aurora.Framework.Settings;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de una configuración de atributos de parametrización en Aurora Platform.
    /// </summary>
    public class AttributeSetting : AuroraAttributeSetting
    {
        /// <summary>
        /// Identificador único del atributo de parametrización.
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Código único del atributo de parametrización.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Nombre descriptivo del atributo de parametrización.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripción detallada del atributo de parametrización.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Tipo de ámbito o alcance del atributo de parametrización.
        /// </summary>
        public string ScopeType { get; set; }

        /// <summary>
        /// Indica si el atributo de parametrización está visible para operaciones del lado cliente.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Indica si los datos del atributo de parametrización pueden ser modificados.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Indica si el atributo de parametrización se encuentra en estado activo.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de una configuración de atributos de parametrización en Aurora Platform.
        /// </summary>
        public class CreateRequest : AuroraAttributeSetting
        {
            /// <summary>
            /// Código único del atributo de parametrización.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(40)]
            public string Code { get; set; }

            /// <summary>
            /// Nombre descriptivo del atributo de parametrización.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Descripción detallada del atributo de parametrización.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(200)]
            public string Description { get; set; }

            /// <summary>
            /// Tipo de ámbito o alcance del atributo de parametrización.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(20)]
            public string ScopeType { get; set; }

            /// <summary>
            /// Indica si el atributo de parametrización está visible para operaciones del lado cliente.
            /// </summary>
            public bool IsVisible { get; set; }

            /// <summary>
            /// Indica si los datos del atributo de parametrización pueden ser modificados.
            /// </summary>
            public bool IsEditable { get; set; }

            /// <summary>
            /// Indica si el atributo de parametrización se encuentra en estado activo.
            /// </summary>
            public bool IsActive { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de la
        /// creación de una configuración de atributos de parametrización en Aurora Platform.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del atributo de parametrización.
            /// </summary>
            public int AttributeId { get; set; }

            /// <summary>
            /// Código único del atributo de parametrización.
            /// </summary>
            public string SettingCode { get; set; }

            /// <summary>
            /// Nombre descriptivo del atributo de parametrización.
            /// </summary>
            public string Name { get; set; }
        }
    }
}