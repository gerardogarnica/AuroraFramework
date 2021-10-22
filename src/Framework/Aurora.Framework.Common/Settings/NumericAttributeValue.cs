using Aurora.Framework.Exceptions;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de valores del atributo de parametrización tipo Numeric.
    /// </summary>
    public class NumericAttributeValue
    {
        /// <summary>
        /// Valor decimal del atributo de parametrización.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase NumericAttributeValue.
        /// </summary>
        public NumericAttributeValue() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase NumericAttributeValue con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML del valor del atributo de parametrización.</param>
        public NumericAttributeValue(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("numericValue")
                    select new
                    {
                        Value = (decimal)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        /// <summary>
        /// Obtiene el valor en formato XML del atributo de parametrización tipo Numeric.
        /// </summary>
        /// <param name="setting">Clase de configuración de atributo de parametrización tipo Numeric.</param>
        /// <returns>Valor en formato XML del atributo de parametrización tipo Numeric.</returns>
        public string GetValueWrapper(NumericAttributeSetting setting)
        {
            if (!Value.IsIntoInterval(setting.MinValue, setting.MaxValue))
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidRangeAttributeValue, Value, setting.MinValue, setting.MaxValue));
            }

            var document = new XDocument(
                new XElement("numericValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}