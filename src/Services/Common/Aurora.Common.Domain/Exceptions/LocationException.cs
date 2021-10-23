using Aurora.Framework.Exceptions;

namespace Aurora.Common.Domain.Exceptions
{
    public class LocationException : BusinessException
    {
        public LocationException(string message, string code)
            : base(message, "LocationException", code) { }
    }

    public class ExistsLocationNameException : LocationException
    {
        public ExistsLocationNameException(string name)
            : base(string.Format("El nombre de la localidad '{0}' ya se encuentra registrado y no puede ser creado de nuevo.", name), "1001") { }
    }

    public class InvalidLocationIdException : LocationException
    {
        public InvalidLocationIdException(int locationId)
            : base(string.Format("El ID de localidad '{0}' no se encuentra registrado.", locationId), "1001") { }
    }

    public class InvalidParentLocationIdException : LocationException
    {
        public InvalidParentLocationIdException(int locationId)
            : base(string.Format("El ID de localidad padre '{0}' no se encuentra registrado.", locationId), "1002") { }
    }

    public class InvalidLocationFirstLevelException : LocationException
    {
        public InvalidLocationFirstLevelException(string divisionName)
            : base(string.Format("El nivel de la división '{0}' es 1 y por tanto la localidad no puede tener una localidad padre.", divisionName), "1003") { }
    }

    public class InvalidLocationLevelException : LocationException
    {
        public InvalidLocationLevelException(string divisionName, int levelNumber)
            : base(string.Format("El nivel de la división '{0}' es '{1}' y por tanto la localidad debe tener una localidad padre.", divisionName, levelNumber), "1004") { }
    }

    public class InvalidParentLocationLevelException : LocationException
    {
        public InvalidParentLocationLevelException(string parentLocationName, string divisionName)
            : base(string.Format("El nivel de la localidad padre '{0}' no corresponde al nivel superior de la división '{1}'.", parentLocationName, divisionName), "1005") { }
    }

    public class InvalidParentLocationCountryException : LocationException
    {
        public InvalidParentLocationCountryException(string parentLocationName, string countryName)
            : base(string.Format("La localidad padre '{0}' no corresponde al país '{1}'.", parentLocationName, countryName), "1006") { }
    }
}