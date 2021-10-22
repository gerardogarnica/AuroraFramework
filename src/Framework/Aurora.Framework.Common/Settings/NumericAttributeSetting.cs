using Aurora.Framework.Exceptions;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de configuración del atributo de parametrización tipo Numeric.
    /// </summary>
    public class NumericAttributeSetting
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
        /// Cantidad de decimales que acepta el valor.
        /// </summary>
        public DecimalsQuantity DecimalsQuantity { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase NumericAttributeSetting.
        /// </summary>
        public NumericAttributeSetting() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase NumericAttributeSetting con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML de la configuración del atributo de parametrización.</param>
        public NumericAttributeSetting(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("numericSetting")
                    select new
                    {
                        MinValue = (decimal)b.Element("minValue"),
                        MaxValue = (decimal)b.Element("maxValue"),
                        DefaultValue = (decimal)b.Element("defaultValue"),
                        DecimalsQuantity = (int)b.Element("decimalsQuantity")
                    };

            MinValue = q.FirstOrDefault().MinValue;
            MaxValue = q.FirstOrDefault().MaxValue;
            DefaultValue = q.FirstOrDefault().DefaultValue;
            DecimalsQuantity = (DecimalsQuantity)q.FirstOrDefault().DecimalsQuantity;
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
                new XElement("numericSetting",
                    new XElement("minValue", MinValue.Round(DecimalsQuantity)),
                    new XElement("maxValue", MaxValue.Round(DecimalsQuantity)),
                    new XElement("defaultValue", DefaultValue.Round(DecimalsQuantity)),
                    new XElement("decimalsQuantity", (int)DecimalsQuantity)));

            return document.ToString();
        }
    }
}