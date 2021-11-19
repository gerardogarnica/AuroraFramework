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
            : this(true) { }

        /// <summary>
        /// Inicializa una instancia de la clase AuroraBaseResponse con la respuesta
        /// del estado de la ejecución de una operación.
        /// </summary>
        /// <param name="isSuccess">Indica si la ejecución de una operación resulta exitosa o no.</param>
        public AuroraBaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Representa el detalle de un error en la ejecución de una operación.
        /// </summary>
        public class ResponseError
        {
            /// <summary>
            /// Categoría del error.
            /// </summary>
            public ResponseErrorCategoryType Category { get; set; }

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

    /// <summary>
    /// Define los tipos de categoría de errores en la ejecución de una operación.
    /// </summary>
    public enum ResponseErrorCategoryType
    {
        /// <summary>
        /// Validación de modelo de datos de entrada.
        /// </summary>
        ModelValidation,

        /// <summary>
        /// Validación de lógica de negocio.
        /// </summary>
        BusinessValidation,

        /// <summary>
        /// Error o excepción no controlada.
        /// </summary>
        Error
    }
}