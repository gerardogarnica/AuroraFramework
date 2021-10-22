using Aurora.Framework.Exceptions;

namespace Aurora.Framework.Settings
{
    /// <summary>
    /// Clase base para una entidad de configuración de atributos de parametrización.
    /// </summary>
    public abstract class AuroraAttributeSetting
    {
        /// <summary>
        /// Tipo de dato AuroraDataType de la configuración del atributo de parametrización.
        /// </summary>
        public AuroraDataType DataType { get; set; }

        /// <summary>
        /// Configuración de tipo Boolean de la configuración del atributo de parametrización.
        /// </summary>
        public BooleanAttributeSetting BooleanSetting { get; set; }

        /// <summary>
        /// Configuración de tipo Catalog de la configuración del atributo de parametrización.
        /// </summary>
        public CatalogAttributeSetting CatalogSetting { get; set; }

        /// <summary>
        /// Configuración de tipo Integer de la configuración del atributo de parametrización.
        /// </summary>
        public IntegerAttributeSetting IntegerSetting { get; set; }

        /// <summary>
        /// Configuración de tipo Money de la configuración del atributo de parametrización.
        /// </summary>
        public MoneyAttributeSetting MoneySetting { get; set; }

        /// <summary>
        /// Configuración de tipo Numeric de la configuración del atributo de parametrización.
        /// </summary>
        public NumericAttributeSetting NumericSetting { get; set; }

        /// <summary>
        /// Configuración de tipo Text de la configuración del atributo de parametrización.
        /// </summary>
        public TextAttributeSetting TextSetting { get; set; }

        /// <summary>
        /// Genera el valor en formato XML de la configuración del atributo de parametrización.
        /// </summary>
        public string GetConfigurationSetting()
        {
            switch (DataType)
            {
                case AuroraDataType.Boolean:
                    if (BooleanSetting == null) throw new PlatformException(ExceptionMessages.InvalidBooleanAttributeSetting);
                    return BooleanSetting.GetConfigurationWrapper();

                case AuroraDataType.Catalog:
                    if (CatalogSetting == null) throw new PlatformException(ExceptionMessages.InvalidCatalogAttributeSetting);
                    return CatalogSetting.GetConfigurationWrapper();

                case AuroraDataType.Integer:
                    if (IntegerSetting == null) throw new PlatformException(ExceptionMessages.InvalidIntegerAttributeSetting);
                    return IntegerSetting.GetConfigurationWrapper();

                case AuroraDataType.Money:
                    if (MoneySetting == null) throw new PlatformException(ExceptionMessages.InvalidMoneyAttributeSetting);
                    return MoneySetting.GetConfigurationWrapper();

                case AuroraDataType.Numeric:
                    if (NumericSetting == null) throw new PlatformException(ExceptionMessages.InvalidNumericAttributeSetting);
                    return NumericSetting.GetConfigurationWrapper();

                case AuroraDataType.Text:
                    if (TextSetting == null) throw new PlatformException(ExceptionMessages.InvalidTextAttributeSetting);
                    return TextSetting.GetConfigurationWrapper();

                default: return null;
            }
        }
    }
}