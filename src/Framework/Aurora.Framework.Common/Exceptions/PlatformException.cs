using System;

namespace Aurora.Framework.Exceptions
{
    /// <summary>
    /// Representa los errores de plataforma Aurora que se producen en una aplicación.
    /// </summary>
    public class PlatformException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase PlatformException.
        /// </summary>
        public PlatformException()
            : base() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase PlatformException con el mensaje de error especificado.
        /// </summary>
        /// <param name="message">Mensaje de error que explica la razón de la excepción.</param>
        public PlatformException(string message)
            : base(message) { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase PlatformException con el mensaje de error especificado
        /// y una referencia a la excepción interna que representa la causa de la excepción.
        /// </summary>
        /// <param name="message">Mensaje de error que explica la razón de la excepción.</param>
        /// <param name="innerException">Excepción que ha provocado que se produzca esta excepción.</param>
        public PlatformException(string message, Exception innerException)
            : base(message, innerException)
        {
            Source = innerException.Source;
        }
    }
}