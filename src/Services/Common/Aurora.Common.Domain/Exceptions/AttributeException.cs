using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class AttributeException : BusinessException
    {
        protected const string AttributeNullMessage = "El registro de atributo no puede ser nulo.";
        protected const string ExistsSettingCodeMessage = "El código de configuración de atributo '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidSettingCodeMessage = "El código de configuración de atributo '{0}' no se encuentra registrado.";
        protected const string InvalidSettingCatalogValueMessage = "Los elementos por defecto no corresponden al catálogo '{0}'.";

        public AttributeException(string errorType, string message)
            : base("AttributeException", errorType, message) { }
    }

    public class AttributeNullException : AttributeException
    {
        public AttributeNullException()
            : base("AttributeNullException", AttributeNullMessage) { }
    }

    public class ExistsSettingCodeException : AttributeException
    {
        public ExistsSettingCodeException(string code)
            : base("ExistsSettingCodeException", string.Format(ExistsSettingCodeMessage, code)) { }
    }

    public class InvalidSettingCodeException : AttributeException
    {
        public InvalidSettingCodeException(string code)
            : base("InvalidSettingCodeException", string.Format(InvalidSettingCodeMessage, code)) { }
    }

    public class InvalidSettingCatalogValueException : AttributeException
    {
        public InvalidSettingCatalogValueException(string code)
            : base("InvalidSettingCatalogValueException", string.Format(InvalidSettingCatalogValueMessage, code)) { }
    }
}