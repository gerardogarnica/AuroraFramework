using System.Collections.Generic;

namespace Aurora.Framework.Services
{
    /// <summary>
    /// Clase que representa las respuestas de errores en la ejecución de operaciones.
    /// </summary>
    public class ErrorDetailResponse
    {
        /// <summary>
        /// Código de respuesta del error.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Categoría de los errores.
        /// </summary>
        public ErrorDetailCategory ErrorCategory { get; }

        /// <summary>
        /// Lista de mensajes de errores en la ejecución de operaciones.
        /// </summary>
        public List<ErrorMessage> Errors { get; }

        /// <summary>
        /// Inicializa una instancia de la clase ErrorDetailResponse con
        /// el código de respuesta y la categoría de errores especificados.
        /// </summary>
        /// <param name="statusCode">Código de respuesta del error.</param>
        /// <param name="category">Categoría de los errores.</param>
        public ErrorDetailResponse(int statusCode, ErrorDetailCategory category)
        {
            StatusCode = statusCode;
            ErrorCategory = category;
            Errors = new List<ErrorMessage>();
        }

        /// <summary>
        /// Agrega un mensaje de error en la ejecución de operaciones.
        /// </summary>
        /// <param name="errorType">Tipo específico del error.</param>
        /// <param name="message">Mensaje específico de error.</param>
        public void AddErrorMessage(string errorType, string message)
        {
            Errors.Add(
                new ErrorMessage()
                {
                    ErrorType = errorType,
                    Message = message
                });
        }

        /// <summary>
        /// Representa el detalle de un mensaje de error en la ejecución de una operación.
        /// </summary>
        public class ErrorMessage
        {
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