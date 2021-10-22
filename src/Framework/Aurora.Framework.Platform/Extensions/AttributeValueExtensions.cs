using Aurora.Framework.Platform.Settings;
using System.Collections.Generic;
using System.Linq;

namespace Aurora.Framework.Platform
{
    /// <summary>
    /// Clase con métodos de extensión para objetos de tipo Aurora.Framework.Platform.Settings.AttributeValue.
    /// </summary>
    public static class AttributeValueExtensions
    {
        /// <summary>
        /// Devuelve un elemento de una lista de atributos de parametrización de acuerdo al código del atributo.
        /// </summary>
        /// <param name="values">Objeto AttributeValue del cual se obtendrá el elemento.</param>
        /// <param name="code">Código del atributo de parametrización.</param>
        public static AttributeValue GetValue(this IEnumerable<AttributeValue> values, string code)
        {
            return values
                .ToList()
                .Find(x => x.Code.Equals(code));
        }
    }
}