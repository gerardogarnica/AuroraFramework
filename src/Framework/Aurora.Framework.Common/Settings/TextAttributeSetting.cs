using Aurora.Framework.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de configuración del atributo de parametrización tipo Text.
    /// </summary>
    public class TextAttributeSetting
    {
        /// <summary>
        /// Longitud mínima del texto.
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// Longitud máxima del texto.
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Valor por defecto. Puede ser nulo.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Patrón Regex del texto. Puede ser nulo.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase TextAttributeSetting.
        /// </summary>
        public TextAttributeSetting() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase TextAttributeSetting con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML de la configuración del atributo de parametrización.</param>
        public TextAttributeSetting(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("textSetting")
                    select new
                    {
                        MinLength = (int)b.Element("minLength"),
                        MaxLength = (int)b.Element("maxLength"),
                        DefaultValue = (string)b.Element("defaultValue"),
                        Pattern = (string)b.Element("pattern")
                    };

            MinLength = q.FirstOrDefault().MinLength;
            MaxLength = q.FirstOrDefault().MaxLength;
            DefaultValue = q.FirstOrDefault().DefaultValue;
            Pattern = q.FirstOrDefault().Pattern;
        }

        internal string GetConfigurationWrapper()
        {
            if (MaxLength < MinLength)
            {
                throw new PlatformException(string.Format(ExceptionMessages.InvalidLengthRangeValueAttributeSetting, MinLength, MaxLength));
            }

            if (!string.IsNullOrWhiteSpace(DefaultValue))
            {
                if (!DefaultValue.Length.IsIntoInterval(MinLength, MaxLength))
                {
                    throw new PlatformException(string.Format(ExceptionMessages.InvalidLengthDefaultValueAttributeSetting, DefaultValue, MinLength, MaxLength));
                }

                if (!string.IsNullOrWhiteSpace(Pattern))
                {
                    if (Regex.IsMatch(DefaultValue, Pattern))
                        throw new PlatformException(string.Format(ExceptionMessages.InvalidPatternValueAttributeSetting, DefaultValue, Pattern));
                }
            }

            var document = new XDocument(
                new XElement("textSetting",
                    new XElement("minLength", MinLength),
                    new XElement("maxLength", MaxLength),
                    new XElement("defaultValue", DefaultValue),
                    new XElement("pattern", Pattern)));

            return document.ToString();
        }
    }
}