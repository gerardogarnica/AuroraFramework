namespace Aurora.Framework
{
    internal static class ExceptionMessages
    {
        internal const string InvalidBooleanAttributeSetting = "No se ha configurado el detalle del atributo tipo Lógico/Binario.";
        internal const string InvalidCatalogAttributeSetting = "No se ha configurado el detalle del atributo tipo Catálogo.";
        internal const string InvalidDefaultValueAttributeSetting = "El valor por defecto '{0}' de la configuración de atributo debe encontrarse en el rango entre el valor mínimo '{1}' y valor máximo '{2}'.";
        internal const string InvalidIntegerAttributeSetting = "No se ha configurado el detalle del atributo tipo Entero.";
        internal const string InvalidItemsCatalogAttributeValue = "La cantidad de items '{0}' del atributo de parametrización no puede superar los '{1}' items.";
        internal const string InvalidLengthAttributeValue = "El valor '{0}' del atributo de parametrización debe poseer una longitud entre {1} y {2} caracteres.";
        internal const string InvalidLengthDefaultValueAttributeSetting = "El valor por defecto '{0}' de la configuración de atributo debe poseer una longitud entre {1} y {2} caracteres.";
        internal const string InvalidLengthRangeValueAttributeSetting = "La longitud máxima '{1}' de la configuración de atributo no puede ser menor a la longitud mínima '{0}'.";
        internal const string InvalidMoneyAttributeSetting = "No se ha configurado el detalle del atributo tipo Moneda.";
        internal const string InvalidNumericAttributeSetting = "No se ha configurado el detalle del atributo tipo Numérico.";
        internal const string InvalidPatternAttributeValue = "El valor '{0}' del atributo de parametrización no cumple con el patrón '{1}'.";
        internal const string InvalidPatternValueAttributeSetting = "El valor por defecto '{0}' de la configuración de atributo no cumple con el patrón '{1}'.";
        internal const string InvalidRangeAttributeValue = "El valor '{0}' del atributo de parametrización debe encontrarse en el rango entre el valor mínimo '{1}' y valor máximo '{2}'.";
        internal const string InvalidRangeValueAttributeSetting = "El valor máximo '{1}' de la configuración de atributo no puede ser menor al valor mínimo '{0}'.";
        internal const string InvalidTextAttributeSetting = "No se ha configurado el detalle del atributo tipo Texto.";
        internal const string OutOfRangeException = "El valor máximo de un rango de comparación no puede ser menor al valor mínimo.";
        internal const string ProtectEncryptionException = "Se produjo un error al cifrar el contenido de la cadena de texto.";
        internal const string ProxyGetResponseException = "Error en la ejecución del servicio invocado. Código de respuesta: {0}. Detalle del error: {1}.";
        internal const string UnprotectEncryptionException = "Se produjo un error al descifrar el contenido de la cadena de texto.";
    }
}