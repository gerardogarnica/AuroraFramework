namespace Aurora.Framework
{
    /// <summary>
    /// Clase con métodos de extensión para objetos de tipo AuroraBaseResponse.
    /// </summary>
    public static class AuroraBaseResponseExtensions
    {
        /// <summary>
        /// Agrega un detalle de error en la ejecución de una operación.
        /// </summary>
        /// <param name="response">Objeto AuroraBaseResponse a evaluar.</param>
        /// <param name="category">Categoría del error.</param>
        /// <param name="errorType">Tipo específico del error.</param>
        /// <param name="message">Mensaje específico de error.</param>
        public static void AddError(
            this AuroraBaseResponse response, ResponseErrorCategoryType category,
            string errorType, string message)
        {
            response.Errors.Add(
                new AuroraBaseResponse.ResponseError()
                {
                    Category = category,
                    ErrorType = errorType,
                    Message = message
                });
        }
    }
}