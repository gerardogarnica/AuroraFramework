using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class AttributeException : BusinessException
    {
        public AttributeException(string message, string code)
            : base(message, "AttributeException", code) { }
    }

    public class ExistsSettingCodeException : AttributeException
    {
        public ExistsSettingCodeException(string code)
            : base(string.Format("El código de configuración de atributo parámetro '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", code), "1001") { }
    }

    public class InvalidSettingCodeException : AttributeException
    {
        public InvalidSettingCodeException(string code)
            : base(string.Format("El código de configuración de atributo parámetro '{0}' no se encuentra registrado.", code), "1002") { }
    }

    public class InvalidSettingCatalogValue : AttributeException
    {
        public InvalidSettingCatalogValue(string code)
            : base(string.Format("Los elementos por defecto no corresponden al catálogo '{0}'.", code), "1002") { }
    }
}