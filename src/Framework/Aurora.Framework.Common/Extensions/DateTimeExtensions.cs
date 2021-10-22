using System;
using System.Globalization;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase con métodos de extensión para objetos de tipo DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Devuelve el número de semana del año del objeto System.DateTime actual.
        /// </summary>
        /// <param name="date">Objeto System.DateTime del cual se obtendrá el número de semana.</param>
        /// <param name="firstDayOfWeek">Valor de System.DayOfWeek que representa el primer día de la semana.</param>
        public static int GetWeekOfYear(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            return new GregorianCalendar().GetWeekOfYear(date, CalendarWeekRule.FirstDay, firstDayOfWeek);
        }

        /// <summary>
        /// Determina si el valor del objeto System.DateTime se encuentra dentro de un intervalo de fechas.
        /// </summary>
        /// <param name="date">Objeto System.DateTime a evaluar.</param>
        /// <param name="beginDate">Fecha inicial del intervalo a comparar.</param>
        /// <param name="endDate">Fecha final del intervalo a comparar.</param>
        public static bool IsIntoInterval(this DateTime date, DateTime beginDate, DateTime endDate)
        {
            return date.Date >= beginDate.Date && date.Date <= endDate.Date;
        }

        /// <summary>
        /// Determina si el valor del objeto System.DateTime se encuentra dentro de un intervalo de fechas con hora.
        /// </summary>
        /// <param name="date">Objeto System.DateTime a evaluar.</param>
        /// <param name="beginDate">Fecha inicial del intervalo a comparar.</param>
        /// <param name="endDate">Fecha final del intervalo a comparar.</param>
        public static bool IsIntoTimeInterval(this DateTime date, DateTime beginDate, DateTime endDate)
        {
            return date >= beginDate && date <= endDate;
        }

        /// <summary>
        /// Convierte el valor del objeto System.DateTime actual en un entero
        /// de 32 bits equivalente utilizando el formato especificado.
        /// </summary>
        /// <param name="date">Objeto System.DateTime que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de fecha de tipo entero.</param>
        public static int ToInt32(this DateTime date, DateFormat format)
        {
            return Convert.ToInt32(date.ToString(format));
        }

        /// <summary>
        /// Convierte el valor del objeto System.DateTime actual en su representación
        /// de cadena equivalente utilizando el formato especificado.
        /// </summary>
        /// <param name="date">Objeto System.DateTime que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de fecha en el que se devolverá la cadena.</param>
        public static string ToString(this DateTime date, DateFormat format)
        {
            return date.ToString(format, string.Empty);
        }

        /// <summary>
        /// Convierte el valor del objeto System.DateTime actual en su representación
        /// de cadena equivalente utilizando el formato especificado.
        /// </summary>
        /// <param name="date">Objeto System.DateTime que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de fecha en el que se devolverá la cadena.</param>
        /// <param name="separator">Cadena de separación de los valores de la fecha a devolver.</param>
        public static string ToString(this DateTime date, DateFormat format, string separator)
        {
            var dayString = date.Day.PadZero(2);
            var monthString = date.Month.PadZero(2);
            var yearString = date.Year.PadZero(4);

            switch (format)
            {
                case DateFormat.DayMonth:
                    return string.Format("{0}{1}{2}", dayString, separator, monthString);

                case DateFormat.DayMonthYear:
                    return string.Format("{0}{1}{2}{3}{4}", dayString, separator, monthString, separator, yearString);

                case DateFormat.MonthDayYear:
                    return string.Format("{0}{1}{2}{3}{4}", monthString, separator, dayString, separator, yearString);

                case DateFormat.MonthYear:
                    return string.Format("{0}{1}{2}", monthString, separator, yearString);

                case DateFormat.YearDayMonth:
                    return string.Format("{0}{1}{2}{3}{4}", yearString, separator, dayString, separator, monthString);

                case DateFormat.YearMonth:
                    return string.Format("{0}{1}{2}", yearString, separator, monthString);

                case DateFormat.YearMonthDay:
                    return string.Format("{0}{1}{2}{3}{4}", yearString, separator, monthString, separator, dayString);

                default: break;
            }

            return string.Empty;
        }

        /// <summary>
        /// Convierte el valor del objeto System.DateTime actual en su representación
        /// de cadena equivalente utilizando el formato especificado.
        /// Si el objeto no tiene valor se devolverá una cadena nula.
        /// </summary>
        /// <param name="date">Objeto System.DateTime que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de fecha de tipo entero.</param>
        public static string ToString(this DateTime? date, DateFormat format)
        {
            return date.ToString(format, string.Empty);
        }

        /// <summary>
        /// Convierte el valor del objeto System.DateTime actual en su representación
        /// de cadena equivalente utilizando el formato especificado.
        /// Si el objeto no tiene valor se devolverá una cadena nula.
        /// </summary>
        /// <param name="date">Objeto System.DateTime que se convertirá al formato especificado.</param>
        /// <param name="format">Formato de fecha de tipo entero.</param>
        /// <param name="separator">Cadena de separación de los valores de la fecha a devolver.</param>
        public static string ToString(this DateTime? date, DateFormat format, string separator)
        {
            return date.HasValue ? Convert.ToDateTime(date).ToString(format, separator) : null;
        }
    }
}