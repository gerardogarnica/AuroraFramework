using Aurora.Framework.Exceptions;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Clase base para una entidad de valores de atributos de parametrización.
    /// </summary>
    public abstract class AuroraAttributeValue
    {
        /// <summary>
        /// Configuración de tipo Boolean del valor del atributo de parametrización.
        /// </summary>
        public BooleanAttributeValue BooleanValue { get; set; }

        /// <summary>
        /// Configuración de tipo Catalog del valor del atributo de parametrización.
        /// </summary>
        public CatalogAttributeValue CatalogValue { get; set; }

        /// <summary>
        /// Configuración de tipo Integer del valor del atributo de parametrización.
        /// </summary>
        public IntegerAttributeValue IntegerValue { get; set; }

        /// <summary>
        /// Configuración de tipo Money del valor del atributo de parametrización.
        /// </summary>
        public MoneyAttributeValue MoneyValue { get; set; }

        /// <summary>
        /// Configuración de tipo Numeric del valor del atributo de parametrización.
        /// </summary>
        public NumericAttributeValue NumericValue { get; set; }

        /// <summary>
        /// Configuración de tipo Text del valor del atributo de parametrización.
        /// </summary>
        public TextAttributeValue TextValue { get; set; }

        /// <summary>
        /// Genera el valor en formato XML del valor del atributo de parametrización.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public string GetConfigurationSetting(AuroraAttributeSetting setting)
        {
            switch (setting.DataType)
            {
                case AuroraDataType.Boolean:
                    if (BooleanValue == null) throw new PlatformException(ExceptionMessages.InvalidBooleanAttributeSetting);
                    return BooleanValue.GetValueWrapper();

                case AuroraDataType.Catalog:
                    if (CatalogValue == null) throw new PlatformException(ExceptionMessages.InvalidCatalogAttributeSetting);
                    return CatalogValue.GetValueWrapper(setting.CatalogSetting);

                case AuroraDataType.Integer:
                    if (IntegerValue == null) throw new PlatformException(ExceptionMessages.InvalidIntegerAttributeSetting);
                    return IntegerValue.GetValueWrapper(setting.IntegerSetting);

                case AuroraDataType.Money:
                    if (MoneyValue == null) throw new PlatformException(ExceptionMessages.InvalidMoneyAttributeSetting);
                    return MoneyValue.GetValueWrapper(setting.MoneySetting);

                case AuroraDataType.Numeric:
                    if (NumericValue == null) throw new PlatformException(ExceptionMessages.InvalidNumericAttributeSetting);
                    return NumericValue.GetValueWrapper(setting.NumericSetting);

                case AuroraDataType.Text:
                    if (TextValue == null) throw new PlatformException(ExceptionMessages.InvalidTextAttributeSetting);
                    return TextValue.GetValueWrapper(setting.TextSetting);

                default: return null;
            }
        }
    }
}