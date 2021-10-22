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
        private readonly string mErrorKeyName;

        #endregion

        #region Propiedades de la clase

        /// <summary>
        /// Categoría o tipo de la excepción.
        /// </summary>
        public string ErrorCategory
        {
            get { return mErrorCategory; }
        }

        /// <summary>
        /// Código identificativo de la excepción.
        /// </summary>
        public string ErrorKeyName
        {
            get { return mErrorKeyName; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Inicializa una nueva instancia de la clase BusinessException.
        /// </summary>
        public BusinessException()
            : base()
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BusinessException con el mensaje de error especificado.
        /// </summary>
        /// <param name="message">Mensaje de error que explica la razón de la excepción.</param>
        /// <param name="category">Categoría o tipo de la excepción.</param>
        /// <param name="keyName">Código identificativo de la excepción.</param>
        public BusinessException(string message, string category, string keyName)
            : base(message)
        {
            mErrorCategory = category;
            mErrorKeyName = keyName;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BusinessException con el mensaje de error especificado
        /// y una referencia a la excepción interna que representa la causa de la excepción.
        /// </summary>
        /// <param name="message">Mensaje de error que explica la razón de la excepción.</param>
        /// <param name="category">Categoría o tipo de la excepción.</param>
        /// <param name="keyName">Código identificativo de la excepción.</param>
        /// <param name="innerException">Excepción que ha provocado que se produzca esta excepción.</param>
        public BusinessException(string message, string category, string keyName, Exception innerException)
            : base(message, innerException)
        {
            mErrorCategory = category;
            mErrorKeyName = keyName;
            Source = innerException.Source;
        }

        #endregion
    }
}