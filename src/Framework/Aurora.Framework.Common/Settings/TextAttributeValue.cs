using Aurora.Framework.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de valores del atributo de parametrización tipo Text.
    /// </summary>
    public class TextAttributeValue
    {
        /// <summary>
        /// Valor texto del atributo de parametrización.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase TextAttributeValue.
        /// </summary>
        public TextAttributeValue() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase TextAttributeValue con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML del valor del atributo de parametrización.</param>
        public TextAttributeValue(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("textValue")
                    select new
                    {
                        Value = (string)b.Element("value")
                    };

            Value = q.FirstOrDefault().Value;
        }

        /// <summary>
        /// Obtiene el valor en formato XML del atributo de parametrización tipo Text.
        /// </summary>
        /// <param name="setting">Clase de configuración de atributo de parametrización tipo Text.</param>
        /// <returns>Valor en formato XML del atributo de parametrización tipo Text.</returns>
        public string GetValueWrapper(TextAttributeSetting setting)
        {
            if (Value != null)
            {
                if (!Value.Length.IsIntoInterval(setting.MinLength, setting.MaxLength))
                {
                    throw new PlatformException(
                        string.Format(ExceptionMessages.InvalidLengthAttributeValue, Value, setting.MinLength, setting.MaxLength));
                }

                if (!string.IsNullOrWhiteSpace(setting.Pattern))
                {
                    if (Regex.IsMatch(Value, setting.Pattern))
                        throw new PlatformException(string.Format(ExceptionMessages.InvalidPatternAttributeValue, Value, setting.Pattern));
                }
            }

            var document = new XDocument(
                new XElement("textValue",
                    new XElement("value", Value)));

            return document.ToString();
        }
    }
}