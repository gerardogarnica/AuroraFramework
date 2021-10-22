using Aurora.Framework.Exceptions;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de valores del atributo de parametrización tipo Money.
    /// </summary>
    public class MoneyAttributeValue
    {
        /// <summary>
        /// Valor monetario del atributo de parametrización.
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase MoneyAttributeValue.
        /// </summary>
        public MoneyAttributeValue() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase MoneyAttributeValue con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML del valor del atributo de parametrización.</param>
        public MoneyAttributeValue(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("moneyValue")
                    select new
                    {
                        Value = (decimal)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        /// <summary>
        /// Obtiene el valor en formato XML del atributo de parametrización tipo Money.
        /// </summary>
        /// <param name="setting">Clase de configuración de atributo de parametrización tipo Money.</param>
        /// <returns>Valor en formato XML del atributo de parametrización tipo Money.</returns>
        public string GetValueWrapper(MoneyAttributeSetting setting)
        {
            if (!Value.IsIntoInterval(setting.MinValue, setting.MaxValue))
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidRangeAttributeValue, Value, setting.MinValue, setting.MaxValue));
            }

            var document = new XDocument(
                new XElement("moneyValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}