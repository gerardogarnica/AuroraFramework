using System;

namespace Aurora.Framework
{
    /// <summary>
    /// Clase con métodos de extensión para objetos de tipo Decimal.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Indica si el valor de la instancia se encuentra dentro de un rango de valores.
        /// </summary>
        /// <param name="value">Instancia que se validará un rango de valores.</param>
        /// <param name="minValue">Valor mínimo de validación del rango.</param>
        /// <param name="maxValue">Valor máximo de validación del rango.</param>
        /// <exception cref="Exceptions.PlatformException">Se devuelve una excepción en caso de que
        /// el valor máximo sea inferior al valor mínimo del rango a comparar.</exception>
        public static bool IsIntoInterval(this decimal value, decimal minValue, decimal maxValue)
        {
            if (maxValue < minValue)
            {
                throw new Exceptions.PlatformException(ExceptionMessages.OutOfRangeException);
            }

            return value >= minValue && value <= maxValue;
        }

        /// <summary>
        /// Redondea un valor decimal a un número específico de dígitos fraccionarios.
        /// </summary>
        /// <param name="value">Valor decimal que se va a redondear.</param>
        /// <param name="decimals">Cantidad de dígitos fraccionarios. El valor por defecto es dos.</param>
        public static decimal Round(this decimal value, DecimalsQuantity decimals = DecimalsQuantity.Two)
        {
            return Math.Round(value, (int)decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Convierte un valor decimal a una cadena de texto en formato de moneda.
        /// </summary>
        /// <param name="value">Valor decimal que se va a convertir a cadena de texto.</param>
        /// <param name="decimals">Cantidad de dígitos fraccionarios.</param>
        public static string ToCurrency(this decimal value, DecimalsQuantity decimals)
        {
            value = value.Round(decimals);

            return string.Format("$ {0}", value);
        }
    }
}