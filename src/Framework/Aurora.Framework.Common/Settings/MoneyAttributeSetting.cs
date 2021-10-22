using Aurora.Framework.Exceptions;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de configuración del atributo de parametrización tipo Money.
    /// </summary>
    public class MoneyAttributeSetting
    {
        /// <summary>
        /// Valor mínimo permitido.
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// Valor máximo permitido.
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// Valor por defecto.
        /// </summary>
        public decimal DefaultValue { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase MoneyAttributeSetting.
        /// </summary>
        public MoneyAttributeSetting() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase MoneyAttributeSetting con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML de la configuración del atributo de parametrización.</param>
        public MoneyAttributeSetting(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("moneySetting")
                    select new
                    {
                        MinValue = (decimal)b.Element("minValue"),
                        MaxValue = (decimal)b.Element("maxValue"),
                        DefaultValue = (decimal)b.Element("defaultValue")
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
                new XElement("moneySetting",
                    new XElement("minValue", MinValue.ToCurrency(DecimalsQuantity.Two)),
                    new XElement("maxValue", MaxValue.ToCurrency(DecimalsQuantity.Two)),
                    new XElement("defaultValue", DefaultValue.ToCurrency(DecimalsQuantity.Two))));

            return document.ToString();
        }
    }
}