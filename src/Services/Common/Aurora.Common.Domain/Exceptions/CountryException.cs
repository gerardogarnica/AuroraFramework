using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class CountryException : BusinessException
    {
        protected const string CountryNullMessage = "El registro de país no puede ser nulo.";
        protected const string ExistsCountryCodeMessage = "El código de país '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidCountryIdMessage = "El ID de país '{0}' no se encuentra registrado.";
        protected const string InvalidCountryDivisionIdMessage = "El ID de división administrativa '{0}' no se encuentra registrado.";
        protected const string InvalidCountryDivisionLevelMessage = "El nivel '{0}' de la división administrativa '{1}' no es válida. El nivel máximo posible de registrar es '{2}'.";
        protected const string DuplicatedCountryCityLevelMessage = "No se puede registrar más de un nivel de ciudad para una división administrativa de país.";
        protected const string DuplicatedCountryDivisionMessage = "Ya existe la división administrativa '{0}' del país {1} y no puede ser registrado de nuevo.";

        public CountryException(string message)
            : base("CountryException", message) { }
    }

    public class CountryNullException : CountryException
    {
        public CountryNullException()
            : base(CountryNullMessage) { }
    }

    public class ExistsCountryCodeException : CountryException
    {
        public ExistsCountryCodeException(string code)
            : base(string.Format(ExistsCountryCodeMessage, code)) { }
    }

    public class InvalidCountryIdException : CountryException
    {
        public InvalidCountryIdException(short countryId)
            : base(string.Format(InvalidCountryIdMessage, countryId)) { }
    }

    public class InvalidCountryDivisionIdException : CountryException
    {
        public InvalidCountryDivisionIdException(short divisionId)
            : base(string.Format(InvalidCountryDivisionIdMessage, divisionId)) { }
    }

    public class InvalidCountryDivisionLevelException : CountryException
    {
        public InvalidCountryDivisionLevelException(string divisionName, int sentLevelNumber, int expectedLevelNumber)
            : base(string.Format(InvalidCountryDivisionLevelMessage, sentLevelNumber, divisionName, expectedLevelNumber)) { }
    }

    public class DuplicatedCountryCityLevelException : CountryException
    {
        public DuplicatedCountryCityLevelException()
            : base(string.Format(DuplicatedCountryCityLevelMessage)) { }
    }

    public class DuplicatedCountryDivisionException : CountryException
    {
        public DuplicatedCountryDivisionException(string countryName, string divisionName)
            : base(string.Format(DuplicatedCountryDivisionMessage, divisionName, countryName)) { }
    }
}