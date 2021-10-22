using System;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase con métodos de extensión para objetos de tipo TimeSpan.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Determina si el valor del objeto System.TimeSpan se encuentra dentro de un intervalo de horas.
        /// </summary>
        /// <param name="span">Objeto System.TimeSpan a evaluar.</param>
        /// <param name="beginSpan">Hora inicial del intervalo a comparar.</param>
        /// <param name="endSpan">Hora final del intervalo a comparar.</param>
        public static bool IsIntoInterval(this TimeSpan span, TimeSpan beginSpan, TimeSpan endSpan)
        {
            return span >= beginSpan && span <= endSpan;
        }

        /// <summary>
        /// Convierte el valor del objeto System.TimeSpan actual en un entero
        /// de 32 bits equivalente utilizando el formato especificado.
        /// </summary>
        /// <param name="span">Objeto System.TimeSpan que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de tiempo.</param>
        public static int ToInt32(this TimeSpan span, TimeFormat format)
        {
            return Convert.ToInt32(span.ToString(format));
        }

        /// <summary>
        /// Convierte el valor del objeto System.TimeSpan actual en su representación
        /// de cadena equivalente utilizando el formato especificado.
        /// </summary>
        /// <param name="span">Objeto System.TimeSpan que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de tiempo.</param>
        public static string ToString(this TimeSpan span, TimeFormat format)
        {
            return span.ToString(format, string.Empty);
        }

        /// <summary>
        /// Convierte el valor del objeto System.TimeSpan actual en su representación
        /// de cadena equivalente utilizando el formato especificado.
        /// </summary>
        /// <param name="span">Objeto System.TimeSpan que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de tiempo.</param>
        /// <param name="separator">Cadena de separación de los valores de la hora a devolver.</param>
        public static string ToString(this TimeSpan span, TimeFormat format, string separator)
        {
            var hoursString = span.Hours.PadZero(2);
            var minutesString = span.Minutes.PadZero(2);
            var secondsString = span.Seconds.PadZero(2);
            var millisecondsString = span.Milliseconds.PadZero(3);

            switch (format)
            {
                case TimeFormat.HourMinute:
                    return string.Format("{0}{1}{2}", hoursString, separator, minutesString);

                case TimeFormat.HourMinuteSecond:
                    return string.Format("{0}{1}{2}{3}{4}", hoursString, separator, minutesString, separator, secondsString);

                case TimeFormat.HourMinuteMillisecond:
                    return string.Format("{0}{1}{2}{3}{4}.{5}", hoursString, separator, minutesString, separator, secondsString, millisecondsString);

                default: break;
            }

            return string.Empty;
        }
    }
}