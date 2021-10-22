namespace Aurora.Framework.Proxies
{
    /// <summary>
    /// Tipo de operación REST de ejecución de un servicio.
    /// </summary>
    public enum RestOperationType
    {
        /// <summary>
        /// Identifica una acción que soporta el método HTTP DELETE.
        /// </summary>
        Delete,

        /// <summary>
        /// Identifica una acción que soporta el método HTTP GET.
        /// </summary>
        Get,

        /// <summary>
        /// Identifica una acción que soporta el método HTTP PATCH.
        /// </summary>
        Patch,

        /// <summary>
        /// Identifica una acción que soporta el método HTTP POST.
        /// </summary>
        Post,

        /// <summary>
        /// Identifica una acción que soporta el método HTTP PUT.
        /// </summary>
        Put
    }
}