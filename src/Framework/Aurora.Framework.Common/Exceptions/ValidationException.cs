using System;
using System.Collections.Generic;

namespace Aurora.Framework.Exceptions
{
    /// <summary>
    /// Representa los errores de validaciones de modelos de entrada.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Lista de mensajes de errores.
        /// </summary>
        public List<string> Errors { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase ValidationException.
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase ValidationException con una lista de mensajes de errores especificados.
        /// </summary>
        /// <param name="errors">Lista de mensajes de errores.</param>
        public ValidationException(IEnumerable<string> errors)
            : this()
        {
            foreach (var error in errors)
            {
                Errors.Add(error);
            }
        }
    }
}