namespace Aurora.Framework
{
    /// <summary>
    /// Clase con métodos de extensión para objetos de tipo Integer.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Indica si el valor de la instancia se encuentra dentro de un rango de valores.
        /// </summary>
        /// <param name="value">Instancia que se validará un rango de valores.</param>
        /// <param name="minValue">Valor mínimo de validación del rango.</param>
        /// <param name="maxValue">Valor máximo de validación del rango.</param>
        /// <exception cref="Exceptions.PlatformException">Se devuelve una excepción en caso de que
        /// el valor máximo sea inferior al valor mínimo del rango a comparar.</exception>
        public static bool IsIntoInterval(this int value, int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new Exceptions.PlatformException(ExceptionMessages.OutOfRangeException);
            }

            return value >= minValue && value <= maxValue;
        }

        /// <summary>
        /// Devuelve una nueva cadena que alinea a la derecha los caracteres de la instancia
        /// e inserta a la izquierda el carácter '0' hasta alcanzar la longitud especificada.
        /// </summary>
        /// <param name="value">Instancia a la que se agregarán caracteres.</param>
        /// <param name="length">Número de caracteres de la cadena resultante.</param>
        public static string PadZero(this int value, int length)
        {
            return value.ToString().PadZero(length);
        }
    }
}