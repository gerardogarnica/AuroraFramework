using Aurora.Framework.Collections;
using Aurora.Framework.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aurora.Framework.Logic.Repositories
{
    /// <summary>
    /// Interface de consultas en repositorios de datos.
    /// </summary>
    public interface IQueryableRepository<T> where T : class, IDataEntity
    {
        /// <summary>
        /// Obtiene un registro de tipo <typeparamref name="T"/> de acuerdo a un criterio de consulta.
        /// </summary>
        /// <param name="filter">Criterio de consulta del registro.</param>
        /// <returns>Devuelve el primer registro de tipo <typeparamref name="T"/> que satisface el criterio de consulta.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Obtiene todos los registros de tipo <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Devuelve todos los registros de tipo <typeparamref name="T"/>.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Obtiene los registros de tipo <typeparamref name="T"/> de acuerdo a un criterio de consulta.
        /// </summary>
        /// <param name="filter">Criterio de consulta de los registros.</param>
        /// <returns>Devuelve los registros de tipo <typeparamref name="T"/> que satisfacen el criterio de consulta.</returns>
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Obtiene los registros de tipo <typeparamref name="T"/> en formato ordenado de acuerdo a un criterio de consulta.
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="filter">Criterio de consulta de los registros.</param>
        /// <param name="orderByExpression">Expresión de ordenamiento.</param>
        /// <param name="descendingOrder">Indica si los registros se obtienen en orden descendente.</param>
        /// <returns>Devuelve los registros de tipo <typeparamref name="T"/>
        /// en formato ordenado que satisfacen el criterio de consulta.</returns>
        Task<IEnumerable<T>> GetListAsync<TS>(
            Expression<Func<T, bool>> filter, Expression<Func<T, TS>> orderByExpression, bool descendingOrder = false);

        /// <summary>
        /// Obtiene los registros de tipo <typeparamref name="T"/> en formato paginado
        /// de acuerdo a un criterio de consulta.
        /// </summary>
        /// <param name="pagedRequest">Requerimiento para obtener registros en formato paginado.</param>
        /// <param name="filter">Criterio de consulta de los registros.</param>
        /// <returns>Devuelve los registros de tipo <typeparamref name="T"/> en formato paginado que satisfacen el criterio de consulta.</returns>
        Task<PagedCollection<T>> GetPagedCollectionAsync(
            PagedViewRequest pagedRequest, Expression<Func<T, bool>> filter);

        /// <summary>
        /// Obtiene los registros de tipo <typeparamref name="T"/> en formato paginado
        /// y ordenado de acuerdo a un criterio de consulta.
        /// </summary>
        /// <typeparam name="TS"></typeparam>
        /// <param name="pagedRequest">Requerimiento para obtener registros en formato paginado.</param>
        /// <param name="filter">Criterio de consulta de los registros.</param>
        /// <param name="orderByExpression">Expresión de ordenamiento.</param>
        /// <param name="descendingOrder">Indica si los registros se obtienen en orden descendente.</param>
        /// <returns>Devuelve los registros de tipo <typeparamref name="T"/>
        /// en formato paginado y ordenado que satisfacen el criterio de consulta.</returns>
        Task<PagedCollection<T>> GetPagedCollectionAsync<TS>(
            PagedViewRequest pagedRequest, Expression<Func<T, bool>> filter,
            Expression<Func<T, TS>> orderByExpression, bool descendingOrder = false);
    }
}