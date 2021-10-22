using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Locations
{
    /// <summary>
    /// Representa una clase que contiene la información de la división administrativa de un país.
    /// </summary>
    public class CountryDivision
    {
        /// <summary>
        /// Identificador único de la división administrativa.
        /// </summary>
        public short DivisionId { get; set; }

        /// <summary>
        /// Identificador único del país.
        /// </summary>
        public short CountryId { get; set; }

        /// <summary>
        /// Nombre de la división administrativa.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Número de nivel en la organización administrativa del país.
        /// </summary>
        public int LevelNumber { get; set; }

        /// <summary>
        /// Indicador si es un nivel que corresponde a una ciudad.
        /// </summary>
        public bool IsCityLevel { get; set; }

        /// <summary>
        /// Indicador si la división administrativa se encuentra en estado activo.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de una división administrativa de un país.
        /// </summary>
        public class SaveRequest
        {
            /// <summary>
            /// Identificador único del país.
            /// </summary>
            [Required]
            public short CountryId { get; set; }

            /// <summary>
            /// Identificador único de la división administrativa.
            /// </summary>
            [Required]
            public short DivisionId { get; set; }

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
}