namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Clase base para respuestas de invocación de servicios.
    /// </summary>
    public abstract class PlatformBaseResponse
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
    }
}