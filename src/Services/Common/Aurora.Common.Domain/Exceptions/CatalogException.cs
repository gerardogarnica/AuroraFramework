using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class CatalogException : BusinessException
    {
        public CatalogException(string message, string code)
            : base(message, "CatalogException", code) { }
    }

    public class ExistsCatalogCodeException : CatalogException
    {
        public ExistsCatalogCodeException(string code)
            : base(string.Format("El código de catálogo '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", code), "1001") { }
    }

    public class InvalidCatalogCodeException : CatalogException
    {
        public InvalidCatalogCodeException(string code)
            : base(string.Format("El código de catálogo '{0}' no se encuentra registrado.", code), "1002") { }
    }

    public class InvalidCatalogItemCodeException : CatalogException
    {
        public InvalidCatalogItemCodeException(string catalogCode, string itemCode)
            : base(string.Format("El elemento '{0}' del catálogo '{1}' no se encuentra registrado.", itemCode, catalogCode), "1002") { }
    }

    public class CatalogNotEditableException : CatalogException
    {
        public CatalogNotEditableException(string code)
            : base(string.Format("El catálogo '{0}' se encuentra configurado para no ser modificado.", code), "1003") { }
    }

    public class CatalogItemNotEditableException : CatalogException
    {
        public CatalogItemNotEditableException(string catalogCode, string itemCode)
            : base(string.Format("El elemento '{0}' del catálogo '{1}' se encuentra configurado para no ser modificado.", itemCode, catalogCode), "1004") { }
    }
}