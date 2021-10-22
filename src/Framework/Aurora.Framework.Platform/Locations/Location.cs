using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Locations
{
    /// <summary>
    /// Representa una clase que contiene la información de una localidad.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Identificador único de la localidad.
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Identificador único de la división administrativa.
        /// </summary>
        public short DivisionId { get; set; }

        /// <summary>
        /// Identificador único de la localidad padre.
        /// </summary>
        public int ParentLocationId { get; set; }

        /// <summary>
        /// Código principal de la localidad.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Código alterno de la localidad.
        /// </summary>
        public string AlternativeCode { get; set; }

        /// <summary>
        /// Nombre de la localidad.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicador si la localidad se encuentra en estado activo.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Lista de localidades hijas.
        /// </summary>
        public IList<Location> Locations { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de una localidad.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Identificador único de división administrativa.
            /// </summary>
            [Required]
            [Range(1, short.MaxValue)]
            public short DivisionId { get; set; }

            /// <summary>
            /// Identificador único de la localidad padre.
            /// Si este valor es 0, se indica que no posee localidad padre.
            /// </summary>
            [Required]
            [Range(0, int.MaxValue)]
            public int ParentLocationId { get; set; }

            /// <summary>
            /// Nombre de la localidad.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Código principal de la localidad, esta propiedad es opcional.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// Código alterno de la localidad, esta propiedad es opcional.
            /// </summary>
            public string AlternativeCode { get; set; }

            /// <summary>
            /// Indicador si la localidad se encuentra en estado activo.
            /// </summary>
            public bool IsActive { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información para la modificación de datos de una localidad.
        /// </summary>
        public class UpdateRequest
        {
            /// <summary>
            /// Identificador único de la localidad.
            /// </summary>
            [Required]
            public int LocationId { get; set; }

            /// <summary>
            /// Nombre de la localidad.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Código principal de la localidad, esta propiedad es opcional.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// Código alterno de la localidad, esta propiedad es opcional.
            /// </summary>
            public string AlternativeCode { get; set; }

            /// <summary>
            /// Indicador si la localidad se encuentra en estado activo.
            /// </summary>
            public bool IsActive { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de localidades.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único de la localidad.
            /// </summary>
            public int LocationId { get; set; }

            /// <summary>
            /// Nombre de la localidad.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Código principal de la localidad.
            /// </summary>
            public string Code { get; set; }
        }
    }
}