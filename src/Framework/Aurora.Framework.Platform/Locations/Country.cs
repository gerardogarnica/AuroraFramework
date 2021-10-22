using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Locations
{
    /// <summary>
    /// Representa una clase que contiene la información de un país.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Identificador único del país.
        /// </summary>
        public short CountryId { get; set; }

        /// <summary>
        /// Nombre del país.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nombre oficial completo del país.
        /// </summary>
        public string OfficialName { get; set; }

        /// <summary>
        /// Código ISO 3166-1 de 2 letras.
        /// </summary>
        public string TwoLettersCode { get; set; }

        /// <summary>
        /// Código ISO 3166-1 de 3 letras.
        /// </summary>
        public string ThreeLettersCode { get; set; }

        /// <summary>
        /// Código ISO 3166-1 numérico de 3 dígitos.
        /// </summary>
        public string ThreeDigitsCode { get; set; }

        /// <summary>
        /// Código de prefijo de internet.
        /// </summary>
        public string InternetPrefix { get; set; }

        /// <summary>
        /// Indicador si el país se encuentra en estado activo.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Divisiones administrativas del país.
        /// </summary>
        public IList<CountryDivision> Divisions { get; set; }

        /// <summary>
        /// Lista de localidades del país.
        /// </summary>
        public IList<Location> Locations { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de un país.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Nombre del país.
            /// </summary>
            [Required]
            [MinLength(1)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Nombre oficial completo del país.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string OfficialName { get; set; }

            /// <summary>
            /// Código ISO 3166-1 de 2 letras.
            /// </summary>
            [Required]
            [StringLength(2)]
            public string TwoLettersCode { get; set; }

            /// <summary>
            /// Código ISO 3166-1 de 3 letras.
            /// </summary>
            [Required]
            [StringLength(3)]
            public string ThreeLettersCode { get; set; }

            /// <summary>
            /// Código ISO 3166-1 numérico de 3 dígitos.
            /// </summary>
            [Required]
            [StringLength(3)]
            public string ThreeDigitsCode { get; set; }

            /// <summary>
            /// Código de prefijo de internet.
            /// </summary>
            [Required]
            [StringLength(3)]
            public string InternetPrefix { get; set; }

            /// <summary>
            /// Indicador si el país se encuentra en estado activo.
            /// </summary>
            public bool IsActive { get; set; }

            /// <summary>
            /// Divisiones administrativas del país.
            /// </summary>
            public IList<CountryDivision> Divisions { get; set; } = new List<CountryDivision>();

            /// <summary>
            /// Representa una clase que contiene la información para la creación de la división administrativa de un país.
            /// </summary>
            public class CountryDivision
            {
                /// <summary>
                /// Nombre de la división administrativa.
                /// </summary>
                [Required]
                [MinLength(1)]
                [MaxLength(50)]
                public string Name { get; set; }

                /// <summary>
                /// Número de nivel en la organización administrativa del país.
                /// </summary>
                [Required]
                [Range(1, 5)]
                public int LevelNumber { get; set; }

                /// <summary>
                /// Indicador si es un nivel que corresponde a una ciudad.
                /// </summary>
                public bool IsCityLevel { get; set; }

                /// <summary>
                /// Indicador si la división administrativa se encuentra en estado activo.
                /// </summary>
                public bool IsActive { get; set; }
            }
        }

        /// <summary>
        /// Representa una clase que contiene la información para la modificación de datos de un país.
        /// </summary>
        public class UpdateRequest
        {
            /// <summary>
            /// Identificador único del país.
            /// </summary>
            [Required]
            public short CountryId { get; set; }

            /// <summary>
            /// Nombre del país.
            /// </summary>
            [Required]
            [MinLength(1)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Nombre oficial completo del país.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string OfficialName { get; set; }

            /// <summary>
            /// Indicador si el país se encuentra en estado activo.
            /// </summary>
            public bool IsActive { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de países.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del país.
            /// </summary>
            public short CountryId { get; set; }

            /// <summary>
            /// Código ISO 3166-1 de 3 letras.
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// Nombre del país.
            /// </summary>
            public string Name { get; set; }
        }
    }
}