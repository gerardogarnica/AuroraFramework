using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class CatalogException : BusinessException
    {
        protected const string CatalogNullMessage = "El registro de catálogo no puede ser nulo.";
        protected const string ExistsCatalogCodeMessage = "El código de catálogo '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidCatalogCodeMessage = "El código de catálogo '{0}' no se encuentra registrado.";
        protected const string InvalidCatalogItemCodeMessage = "El elemento '{0}' del catálogo '{1}' no se encuentra registrado.";
        protected const string CatalogNotEditableMessage = "El catálogo '{0}' se encuentra configurado para no ser modificado.";
        protected const string CatalogItemNotEditableMessage = "El elemento '{0}' del catálogo '{1}' se encuentra configurado para no ser modificado.";

        public CatalogException(string message)
            : base("CatalogException", message) { }
    }

    public class CatalogNullException : CatalogException
    {
        public CatalogNullException()
            : base(CatalogNullMessage) { }
    }

    public class ExistsCatalogCodeException : CatalogException
    {
        public ExistsCatalogCodeException(string code)
            : base(string.Format(ExistsCatalogCodeMessage, code)) { }
    }

    public class InvalidCatalogCodeException : CatalogException
    {
        public InvalidCatalogCodeException(string code)
            : base(string.Format(InvalidCatalogCodeMessage, code)) { }
    }

    public class InvalidCatalogItemCodeException : CatalogException
    {
        public InvalidCatalogItemCodeException(string catalogCode, string itemCode)
            : base(string.Format(InvalidCatalogItemCodeMessage, itemCode, catalogCode)) { }
    }

    public class CatalogNotEditableException : CatalogException
    {
        public CatalogNotEditableException(string code)
            : base(string.Format(CatalogNotEditableMessage, code)) { }
    }

    public class CatalogItemNotEditableException : CatalogException
    {
        public CatalogItemNotEditableException(string catalogCode, string itemCode)
            : base(string.Format(CatalogItemNotEditableMessage, itemCode, catalogCode)) { }
    }
}