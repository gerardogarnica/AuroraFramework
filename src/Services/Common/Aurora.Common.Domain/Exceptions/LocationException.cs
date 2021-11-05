using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class LocationException : BusinessException
    {
        protected const string LocationNullMessage = "El registro de localidad no puede ser nulo.";
        protected const string ExistsLocationNameMessage = "El nombre de la localidad '{0}' ya se encuentra registrado y no puede ser creado de nuevo.";
        protected const string InvalidLocationIdMessage = "El ID de localidad '{0}' no se encuentra registrado.";
        protected const string InvalidParentLocationIdMessage = "El ID de localidad padre '{0}' no se encuentra registrado.";
        protected const string InvalidLocationFirstLevelMessage = "El nivel de la división '{0}' es 1 y por tanto la localidad no puede tener una localidad padre.";
        protected const string InvalidLocationLevelMessage = "El nivel de la división '{0}' es '{1}' y por tanto la localidad debe tener una localidad padre.";
        protected const string InvalidParentLocationLevelMessage = "El nivel de la localidad padre '{0}' no corresponde al nivel superior de la división '{1}'.";
        protected const string InvalidParentLocationCountryMessage = "La localidad padre '{0}' no corresponde al país '{1}'.";

        public LocationException(string errorType, string message)
            : base("LocationException", errorType, message) { }
    }

    public class LocationNullException : LocationException
    {
        public LocationNullException()
            : base("LocationNullException", LocationNullMessage) { }
    }

    public class ExistsLocationNameException : LocationException
    {
        public ExistsLocationNameException(string name)
            : base("ExistsLocationNameException", string.Format(ExistsLocationNameMessage, name)) { }
    }

    public class InvalidLocationIdException : LocationException
    {
        public InvalidLocationIdException(int locationId)
            : base("InvalidLocationIdException", string.Format(InvalidLocationIdMessage, locationId)) { }
    }

    public class InvalidParentLocationIdException : LocationException
    {
        public InvalidParentLocationIdException(int locationId)
            : base("InvalidParentLocationIdException", string.Format(InvalidParentLocationIdMessage, locationId)) { }
    }

    public class InvalidLocationFirstLevelException : LocationException
    {
        public InvalidLocationFirstLevelException(string divisionName)
            : base("InvalidLocationFirstLevelException", string.Format(InvalidLocationFirstLevelMessage, divisionName)) { }
    }

    public class InvalidLocationLevelException : LocationException
    {
        public InvalidLocationLevelException(string divisionName, int levelNumber)
            : base("InvalidLocationLevelException", string.Format(InvalidLocationLevelMessage, divisionName, levelNumber)) { }
    }

    public class InvalidParentLocationLevelException : LocationException
    {
        public InvalidParentLocationLevelException(string parentLocationName, string divisionName)
            : base("InvalidParentLocationLevelException", string.Format(InvalidParentLocationLevelMessage, parentLocationName, divisionName)) { }
    }

    public class InvalidParentLocationCountryException : LocationException
    {
        public InvalidParentLocationCountryException(string parentLocationName, string countryName)
            : base("InvalidParentLocationCountryException", string.Format(InvalidParentLocationCountryMessage, parentLocationName, countryName)) { }
    }
}