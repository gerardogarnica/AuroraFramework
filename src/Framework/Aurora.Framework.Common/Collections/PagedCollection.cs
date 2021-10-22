using System.Collections.Generic;
using System.Linq;

namespace Aurora.Framework.Collections
{
    /// <summary>
    /// Representa una colección de registros en formato paginado.
    /// </summary>
    /// <typeparam name="T">Entidad de transferencia de datos.</typeparam>
    public class PagedCollection<T> where T : class
    {
        /// <summary>
        /// Elementos de la colección de registros de tipo de entidad de transferencia de datos.
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        /// Número de página actual de la colección de registros.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Número del total de registros
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Número del total de páginas.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Determina si la colección contiene registros.
        /// </summary>
        public bool HasItems
        {
            get
            {
                return Items != null && Items.Any();
            }
        }

        /// <summary>
        /// Devuelve una cadena de texto con la información que representa al objeto actual.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Page {0} of {1}. Total records: {2}.", CurrentPage, TotalPages, TotalRecords);
        }
    }
}