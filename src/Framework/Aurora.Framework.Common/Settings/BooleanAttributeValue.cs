using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de valores del atributo de parametrización tipo Boolean.
    /// </summary>
    public class BooleanAttributeValue
    {
        /// <summary>
        /// Valor boolean del atributo de parametrización.
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BooleanAttributeValue.
        /// </summary>
        public BooleanAttributeValue() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BooleanAttributeValue con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML del valor del atributo de parametrización.</param>
        public BooleanAttributeValue(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("booleanValue")
                    select new
                    {
                        Value = (bool)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        /// <summary>
        /// Obtiene el valor en formato XML del atributo de parametrización tipo Boolean.
        /// </summary>
        /// <returns>Valor en formato XML del atributo de parametrización tipo Boolean.</returns>
        public string GetValueWrapper()
        {
            var document = new XDocument(
                new XElement("booleanValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}