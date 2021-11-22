namespace Aurora.Framework.Services
{
    /// <summary>
    /// Define la categoría del detalle de los errores en la ejecución de una operación.
    /// </summary>
    public enum ErrorDetailCategory
    {
        /// <summary>
        /// Validación de modelo de datos de entrada.
        /// </summary>
        ModelValidation = 1,

        /// <summary>
        /// Validación de lógica de negocio.
        /// </summary>
        BusinessValidation = 2,

        /// <summary>
        /// Error o excepción no controlada.
        /// </summary>
        Error = 3
    }
}