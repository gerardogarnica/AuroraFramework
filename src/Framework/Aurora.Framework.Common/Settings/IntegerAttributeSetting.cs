using Aurora.Framework.Exceptions;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de configuración del atributo de parametrización tipo Integer.
    /// </summary>
    public class IntegerAttributeSetting
    {
        /// <summary>
        /// Valor mínimo permitido.
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// Valor máximo permitido.
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// Valor por defecto.
        /// </summary>
        public int DefaultValue { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase IntegerAttributeSetting.
        /// </summary>
        public IntegerAttributeSetting() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase IntegerAttributeSetting con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML de la configuración del atributo de parametrización.</param>
        public IntegerAttributeSetting(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("integerSetting")
                    select new
                    {
                        MinValue = (int)b.Element("minValue"),
                        MaxValue = (int)b.Element("maxValue"),
                        DefaultValue = (int)b.Element("defaultValue")
                    };

            MinValue = q.FirstOrDefault().MinValue;
            MaxValue = q.FirstOrDefault().MaxValue;
            DefaultValue = q.FirstOrDefault().DefaultValue;
        }

        internal string GetConfigurationWrapper()
        {
            if (MaxValue < MinValue)
            {
                throw new PlatformException(string.Format(ExceptionMessages.InvalidRangeValueAttributeSetting, MinValue, MaxValue));
            }

            if (!DefaultValue.IsIntoInterval(MinValue, MaxValue))
            {
                throw new PlatformException(string.Format(ExceptionMessages.InvalidDefaultValueAttributeSetting, DefaultValue, MinValue, MaxValue));
            }

            var document = new XDocument(
                new XElement("integerSetting",
                    new XElement("minValue", MinValue),
                    new XElement("maxValue", MaxValue),
                    new XElement("defaultValue", DefaultValue)));

            return document.ToString();
        }
    }
}