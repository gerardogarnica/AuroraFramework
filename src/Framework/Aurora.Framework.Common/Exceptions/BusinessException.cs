using System;

namespace Aurora.Framework.Exceptions
{
    /// <summary>
    /// Representa los errores de lógica de negocios que se producen en una aplicación.
    /// </summary>
    public class BusinessException : Exception
    {
        #region Miembros privados de la clase

        private readonly string mErrorCategory;
        private readonly string mErrorType;

        #endregion

        #region Propiedades de la clase

        /// <summary>
        /// Categoría o descripción de la excepción.
        /// </summary>
        public string ErrorCategory
        {
            get { return mErrorCategory; }
        }

        /// <summary>
        /// Tipo de la excepción.
        /// </summary>
        public string ErrorType
        {
            get { return mErrorType; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase BusinessException.
        /// </summary>
        public BusinessException()
            : base() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BusinessException con el mensaje de error especificado.
        /// </summary>
        /// <param name="category">Categoría o descripción de la excepción.</param>
        /// <param name="errorType">Tipo de la excepción.</param>
        /// <param name="message">Mensaje de error que explica la razón de la excepción.</param>
        public BusinessException(string category, string errorType, string message)
            : base(message)
        {
            mErrorType = errorType;
            mErrorCategory = category;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BusinessException con el mensaje de error especificado
        /// y una referencia a la excepción interna que representa la causa de la excepción.
        /// </summary>
        /// <param name="category">Categoría o tipo de la excepción.</param>
        /// <param name="errorType">Código identificativo de la excepción.</param>
        /// <param name="message">Mensaje de error que explica la razón de la excepción.</param>
        /// <param name="innerException">Excepción que ha provocado que se produzca esta excepción.</param>
        public BusinessException(string category, string errorType, string message, Exception innerException)
            : base(message, innerException)
        {
            mErrorType = errorType;
            mErrorCategory = category;
            Source = innerException.Source;
        }

        #endregion
    }
}