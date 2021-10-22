using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de configuración del atributo de parametrización tipo Boolean.
    /// </summary>
    public class BooleanAttributeSetting
    {
        /// <summary>
        /// Valor por defecto.
        /// </summary>
        public bool DefaultValue { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BooleanAttributeSetting.
        /// </summary>
        public BooleanAttributeSetting() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase BooleanAttributeSetting con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML de la configuración del atributo de parametrización.</param>
        public BooleanAttributeSetting(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("booleanSetting")
                    select new
                    {
                        DefaultValue = (bool)b.Element("defaultValue")
                    };

            DefaultValue = q.FirstOrDefault().DefaultValue;
        }

        internal string GetConfigurationWrapper()
        {
            var document = new XDocument(
                new XElement("booleanSetting",
                    new XElement("defaultValue", DefaultValue)));

            return document.ToString();
        }
    }
}