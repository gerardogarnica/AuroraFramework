using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class CountryException : BusinessException
    {
        public CountryException(string message, string code)
            : base(message, "CountryException", code) { }
    }

    public class ExistsCountryCodeException : CountryException
    {
        public ExistsCountryCodeException(string code)
            : base(string.Format("El código de país '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", code), "1001") { }
    }

    public class InvalidCountryIdException : CountryException
    {
        public InvalidCountryIdException(short countryId)
            : base(string.Format("El ID de país '{0}' no se encuentra registrado.", countryId), "1002") { }
    }

    public class InvalidCountryDivisionIdException : CountryException
    {
        public InvalidCountryDivisionIdException(short divisionId)
            : base(string.Format("El ID de división administrativa '{0}' no se encuentra registrado.", divisionId), "1003") { }
    }

    public class InvalidCountryDivisionLevelException : CountryException
    {
        public InvalidCountryDivisionLevelException(string divisionName, int sentLevelNumber, int expectedLevelNumber)
            : base(string.Format("El nivel '{0}' de la división administrativa '{1}' no es válida. El nivel máximo posible de registrar es '{2}'.", sentLevelNumber, divisionName, expectedLevelNumber), "1004") { }
    }

    public class DuplicatedCountryCityLevelException : CountryException
    {
        public DuplicatedCountryCityLevelException()
            : base(string.Format("No se puede registrar más de un nivel de ciudad para una división administrativa de país."), "1004") { }
    }

    public class DuplicatedCountryDivisionException : CountryException
    {
        public DuplicatedCountryDivisionException(string countryName, string divisionName)
            : base(string.Format("Ya existe la división administrativa '{0}' del país {1} y no puede ser registrado de nuevo.", divisionName, countryName), "1005") { }
    }
}