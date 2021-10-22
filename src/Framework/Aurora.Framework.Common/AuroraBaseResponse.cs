namespace Aurora.Framework
{
    /// <summary>
    /// Clase base para respuestas de ejecución de operaciones.
    /// </summary>
    public abstract class AuroraBaseResponse
    {
        /// <summary>
        /// Indica si la ejecución de la operación resulta exitosa o no.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Código de respuesta de la operación.
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// Mensaje de respuesta de la operación.
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Inicializa una instancia de la clase AuroraBaseResponse.
        /// </summary>
        /// <param name="success">Indica si la ejecución de la operación resulta exitosa o no.</param>
        /// <param name="code">Código de respuesta de la operación.</param>
        /// <param name="message">Mensaje de respuesta de la operación.</param>
        public AuroraBaseResponse(bool success, string code, string message)
        {
            IsSuccess = success;
            ResponseCode = code;
            ResponseMessage = message;
        }
    }
}