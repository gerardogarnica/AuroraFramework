using Aurora.Framework.Collections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Framework.Logic
{
    /// <summary>
    /// Clase con métodos de extensión para obtención de una colección de registros en formato paginado.
    /// </summary>
    public static class PagingExtensions
    {
        /// <summary>
        /// Asíncronamente crea una colección de registros en formato paginado PagedCollection<typeparamref name="T"/>
        /// desde una lista IQueryable<typeparamref name="T"/> para enumerarlo asíncronamente.
        /// </summary>
        /// <typeparam name="T">Entidad de transferencia de datos.</typeparam>
        /// <param name="query">Lista IQueryable desde la cual se creará una colección de registros en formato paginado PagedCollection.</param>
        /// <param name="pageIndex">Número de página de la colección de registros a retornar. El índice de la primera página es 0.</param>
        /// <param name="pageSize">Número de elementos de la página de la colección de registros a retornar.</param>
        /// <returns></returns>
        public static async Task<PagedCollection<T>> ToPagedCollectionAsync<T>(
            this IQueryable<T> query, int pageIndex, int pageSize) where T : class
        {
            var startingElement = 0;
            if (pageIndex > 0) startingElement = pageIndex * pageSize;

            var result = new PagedCollection<T>
            {
                Items = await query.Skip(startingElement).Take(pageSize).ToListAsync(),
                TotalRecords = await query.CountAsync(),
                CurrentPage = pageIndex + 1
            };

            if (result.TotalRecords > 0)
            {
                result.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.TotalRecords) / pageSize));
            }

            return result;
        }
    }
}