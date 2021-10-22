using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Representa una clase que contiene la información de configuración del atributo de parametrización tipo Catalog.
    /// </summary>
    public class CatalogAttributeSetting
    {
        /// <summary>
        /// Código del catálogo.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Indica si el atributo de parametrización permite la selección de varios valores.
        /// </summary>
        public bool AllowMultipleValues { get; set; }

        /// <summary>
        /// Número máximo de valores seleccionables en caso de que la propiedad AllowMultipleValues sea True.
        /// Si el valor es cero (0) no existe un límite.
        /// </summary>
        public int MaxSelectedItems { get; set; }

        /// <summary>
        /// Códigos de elementos seleccionados por defecto.
        /// </summary>
        public IList<string> DefaultItemCodes { get; set; }

        /// <summary>
        /// Indica si se presentan los elementos del catálogo inactivos.
        /// </summary>
        public bool ShowInactiveItems { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase CatalogAttributeSetting.
        /// </summary>
        public CatalogAttributeSetting() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase CatalogAttributeSetting con el valor
        /// en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        /// <param name="xmlConfiguration">Valor en formato XML de la configuración del atributo de parametrización.</param>
        public CatalogAttributeSetting(string xmlConfiguration)
        {
            var document = XDocument.Parse(xmlConfiguration);

            var q = from b in document.Descendants("catalogSetting")
                    select new
                    {
                        Code = (string)b.Element("code"),
                        AllowMultipleValues = (bool)b.Element("allowMultipleValues"),
                        MaxSelectedItem = (int)b.Element("maxSelectedItem"),
                        DefaultItemCodes = (string)b.Element("defaultItemCodes"),
                        ShowInactiveItems = (bool)b.Element("showInactiveItems")
                    };

            Code = q.FirstOrDefault().Code;
            AllowMultipleValues = q.FirstOrDefault().AllowMultipleValues;
            MaxSelectedItems = q.FirstOrDefault().MaxSelectedItem;
            DefaultItemCodes = q.FirstOrDefault().DefaultItemCodes.Split(";").ToList();
            ShowInactiveItems = q.FirstOrDefault().ShowInactiveItems;
        }

        internal string GetConfigurationWrapper()
        {
            var codes = string.Join(";", DefaultItemCodes.ToArray());

            var document = new XDocument(
                new XElement("catalogSetting",
                    new XElement("code", Code),
                    new XElement("allowMultipleValues", AllowMultipleValues),
                    new XElement("maxSelectedItem", MaxSelectedItems),
                    new XElement("defaultItemCodes", codes),
                    new XElement("showInactiveItems", ShowInactiveItems)));

            return document.ToString();
        }
    }
}