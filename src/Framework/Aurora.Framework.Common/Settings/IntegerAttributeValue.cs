using Aurora.Framework.Exceptions;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de valores del atributo de parametrización tipo Integer.
    /// </summary>
    public class IntegerAttributeValue
    {
        /// <summary>
        /// Valor entero del atributo de parametrización.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase IntegerAttributeValue.
        /// </summary>
        public IntegerAttributeValue() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase IntegerAttributeValue con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML del valor del atributo de parametrización.</param>
        public IntegerAttributeValue(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("integerValue")
                    select new
                    {
                        Value = (int)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        /// <summary>
        /// Obtiene el valor en formato XML del atributo de parametrización tipo Integer.
        /// </summary>
        /// <param name="setting">Clase de configuración de atributo de parametrización tipo Integer.</param>
        /// <returns>Valor en formato XML del atributo de parametrización tipo Integer.</returns>
        public string GetValueWrapper(IntegerAttributeSetting setting)
        {
            if (!Value.IsIntoInterval(setting.MinValue, setting.MaxValue))
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidRangeAttributeValue, Value, setting.MinValue, setting.MaxValue));
            }

            var document = new XDocument(
                new XElement("integerValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}