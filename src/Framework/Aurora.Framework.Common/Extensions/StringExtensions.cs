using System;
using System.Globalization;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase con métodos de extensión para objetos de tipo String.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Obtiene el último elemento de una matriz de cadenas del objeto System.String actual
        /// delimitadas por un texto separador.
        /// </summary>
        /// <param name="value">Instancia de la que se obtendrá el último elemento de la matriz.</param>
        /// <param name="separator">Texto separador que delimita las subcadenas de la cadena de texto principal.</param>
        public static string GetLastSplit(this string value, string separator)
        {
            var stringSeparator = new string[] { separator };
            var tokens = value.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

            return tokens[tokens.Length - 1];
        }

        /// <summary>
        /// Devuelve una nueva cadena que alinea a la derecha los caracteres de la instancia
        /// e inserta a la izquierda el carácter '0' hasta alcanzar la longitud especificada.
        /// </summary>
        /// <param name="value">Instancia a la que se agregarán caracteres.</param>
        /// <param name="length">Número de caracteres de la cadena resultante.</param>
        public static string PadZero(this string value, int length)
        {
            var newValue = value.PadLeft(length, '0');

            if (newValue.Length > length)
            {
                return newValue.Substring(newValue.Length - length, length);
            }

            return newValue;
        }

        /// <summary>
        /// Convierte el valor del objeto System.String actual en un objeto System.Boolean.
        /// </summary>
        /// <param name="value">Cadena que se convertirá a System.Boolean.</param>
        /// <returns>True si la cadena es igual a "S", "SI", "V", "VERDADERO", "Y", "YES", "T", "TRUE", "1".
        /// False si la cadena es igual a "N", "NO", "F", "FALSE", "F", "FALSE", "0".
        /// Nulo en cualquier otro caso</returns>
        public static bool? ToBoolean(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var booleanTrueStrings = new string[] { "S", "SI", "V", "VERDADERO", "Y", "YES", "T", "TRUE", "1" };
                var booleanFalseStrings = new string[] { "N", "NO", "F", "FALSE", "F", "FALSE", "0" };

                if (Array.IndexOf<string>(booleanTrueStrings, value.Trim().ToUpper()) > -1) return true;
                if (Array.IndexOf<string>(booleanFalseStrings, value.Trim().ToUpper()) > -1) return false;
            }

            return null;
        }

        /// <summary>
        /// Convierte el valor del objeto System.String actual en un objeto System.DateTime.
        /// </summary>
        /// <param name="value">Cadena que se convertirá a System.DateTime.</param>
        /// <param name="format">Formato requerido para la conversión de la cadena.</param>
        /// <returns>Un objeto System.DateTime en caso de que se pueda realizar la conversión
        /// en el formato requerido. En caso contrario se devuelve nulo.</returns>
        public static DateTime? ToDateTime(this string value, string format)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out var returnDate))
                    return returnDate;
            }

            return null;
        }
    }
}