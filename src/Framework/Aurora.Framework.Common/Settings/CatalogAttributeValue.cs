using Aurora.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de valores del atributo de parametrización tipo Catalog.
    /// </summary>
    public class CatalogAttributeValue
    {
        /// <summary>
        /// Códigos de elementos seleccionados del atributo de parametrización.
        /// </summary>
        public IList<string> ItemCodes { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase CatalogAttributeValue.
        /// </summary>
        public CatalogAttributeValue() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase CatalogAttributeValue con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML del valor del atributo de parametrización.</param>
        public CatalogAttributeValue(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("catalogValue")
                    select new
                    {
                        ItemCodes = (string)b.Element("itemCodes")
                    };

            ItemCodes = q.FirstOrDefault().ItemCodes.Split(";").ToList();
        }

        /// <summary>
        /// Obtiene el valor en formato XML del atributo de parametrización tipo Catalog.
        /// </summary>
        /// <param name="setting">Clase de configuración de atributo de parametrización tipo Catalog.</param>
        /// <returns>Valor en formato XML del atributo de parametrización tipo Catalog.</returns>
        public string GetValueWrapper(CatalogAttributeSetting setting)
        {
            if (setting.AllowMultipleValues && ItemCodes.Count > setting.MaxSelectedItems)
            {
                throw new PlatformException(
                    string.Format(ExceptionMessages.InvalidItemsCatalogAttributeValue, ItemCodes.Count, setting.MaxSelectedItems));
            }

            var codes = string.Join(";", ItemCodes.ToArray());

            var document = new XDocument(
                new XElement("catalogValue",
                    new XElement("itemCodes", codes)));

            return document.ToString();
        }
    }
}