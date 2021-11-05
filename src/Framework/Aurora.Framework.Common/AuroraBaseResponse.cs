using System.Collections.Generic;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase base para respuestas de ejecución de operaciones.
    /// </summary>
    public class AuroraBaseResponse
    {
        /// <summary>
        /// Indica si la ejecución de una operación resulta exitosa o no.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Lista de errores de una ejecución de la operación.
        /// </summary>
        public List<ResponseError> Errors { get; set; } = new List<ResponseError>();

        /// <summary>
        /// Inicializa una instancia de la clase AuroraBaseResponse
        /// como ejecución exitosa de una operación.
        /// </summary>
        public AuroraBaseResponse()
        {
            IsSuccess = true;
        }

        /// <summary>
        /// Inicializa una instancia de la clase AuroraBaseResponse
        /// como ejecución con errores de una operación.
        /// </summary>
        /// <param name="errorType">Tipo específico del error.</param>
        /// <param name="message">Mensaje específico de error.</param>
        public AuroraBaseResponse(string errorType, string message)
            : this(errorType, errorType, message) { }

        /// <summary>
        /// Inicializa una instancia de la clase AuroraBaseResponse
        /// como ejecución con errores de una operación.
        /// </summary>
        /// <param name="errorCategory">Categoría o descripción del error.</param>
        /// <param name="errorType">Tipo específico del error.</param>
        /// <param name="message">Mensaje específico de error.</param>
        public AuroraBaseResponse(string errorCategory, string errorType, string message)
        {
            IsSuccess = false;
            Errors.Add(
                new ResponseError()
                {
                    Category = errorCategory,
                    ErrorType = errorType,
                    Message = message
                });
        }

        /// <summary>
        /// Representa el detalle de un error en la ejecución de una operación.
        /// </summary>
        public class ResponseError
        {
            /// <summary>
            /// Categoría o descripción del error.
            /// </summary>
            public string Category { get; set; }

            /// <summary>
            /// Tipo específico del error.
            /// </summary>
            public string ErrorType { get; set; }

            /// <summary>
            /// Mensaje específico de error.
            /// </summary>
            public string Message { get; set; }
        }
    }
}