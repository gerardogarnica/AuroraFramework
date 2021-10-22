using Aurora.Framework.Settings;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información del valor de un atributo de parametrización en Aurora Platform.
    /// </summary>
    public class AttributeValue : AuroraAttributeValue
    {
        /// <summary>
        /// Identificador único del atributo de parametrización.
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Identificador del registro de relación. Si no existe, es cero (0).
        /// </summary>
        public int RelationshipId { get; set; }

        /// <summary>
        /// Código del atributo de parametrización.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tipo de dato AuroraDataType del valor del atributo de parametrización.
        /// </summary>
        public AuroraDataType DataType { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para almacenar el valor de un atributo de parametrización en Aurora Platform.
        /// </summary>
        public class SaveRequest : AuroraAttributeValue
        {
            /// <summary>
            /// Código único del atributo de parametrización.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(40)]
            public string Code { get; set; }

            /// <summary>
            /// Identificador del registro de relación. Si no existe, es cero (0).
            /// </summary>
            public int RelationshipId { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de la
        /// operaciones de valores de atributos de parametrización en Aurora Platform.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del atributo de parametrización.
            /// </summary>
            public int AttributeId { get; set; }

            /// <summary>
            /// Identificador del registro de relación. Si no existe, es cero (0).
            /// </summary>
            public int RelationshipId { get; set; }
        }
    }
}