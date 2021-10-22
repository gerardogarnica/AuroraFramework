using Aurora.Framework.Collections;
using Aurora.Framework.Logic.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aurora.Framework.Logic.Repositories
{
    /// <summary>
    /// Clase base de implementación de interfaces de consultas y ejecución de operaciones en repositorios de datos.
    /// </summary>
    public abstract class DataRepository<T> : IQueryableRepository<T>, IWriteableRepository<T>, IRemovableRepository<T> where T : class, IDataEntity
    {
        #region Miembros privados de la clase

        private readonly DbContext _dataContext;

        #endregion

        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase DataRepository.
        /// </summary>
        /// <param name="context">Instancia que representa una sesión de acceso a datos.</param>
        public DataRepository(DbContext context)
        {
            _dataContext = context;
        }

        #endregion

        #region Implementación de interface IQueryableRepository

        async Task<T> IQueryableRepository<T>.GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dataContext
                .Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(filter);
        }

        async Task<IEnumerable<T>> IQueryableRepository<T>.GetAllAsync()
        {
            return await _dataContext
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        async Task<IEnumerable<T>> IQueryableRepository<T>.GetListAsync(Expression<Func<T, bool>> filter)
        {
            return await _dataContext
                .Set<T>()
                .AsNoTracking()
                .Where(filter)
                .ToListAsync();
        }

        async Task<IEnumerable<T>> IQueryableRepository<T>.GetListAsync<TS>(
            Expression<Func<T, bool>> filter, Expression<Func<T, TS>> orderByExpression, bool descendingOrder)
        {
            return descendingOrder
                ? await _dataContext
                    .Set<T>()
                    .AsNoTracking()
                    .Where(filter)
                    .OrderByDescending(orderByExpression)
                    .ToListAsync()
                : await _dataContext
                    .Set<T>()
                    .AsNoTracking()
                    .Where(filter)
                    .OrderBy(orderByExpression)
                    .ToListAsync();
        }

        async Task<PagedCollection<T>> IQueryableRepository<T>.GetPagedCollectionAsync(
            PagedViewRequest pagedRequest, Expression<Func<T, bool>> filter)
        {
            return await _dataContext
                .Set<T>()
                .AsNoTracking()
                .Where(filter)
                .ToPagedCollectionAsync(pagedRequest.PageIndex, pagedRequest.PageSize);
        }

        async Task<PagedCollection<T>> IQueryableRepository<T>.GetPagedCollectionAsync<TS>(
            PagedViewRequest pagedRequest, Expression<Func<T, bool>> filter,
            Expression<Func<T, TS>> orderByExpression, bool descendingOrder)
        {
            return descendingOrder
                ? await _dataContext
                    .Set<T>()
                    .AsNoTracking()
                    .Where(filter)
                    .OrderByDescending(orderByExpression)
                    .ToPagedCollectionAsync(pagedRequest.PageIndex, pagedRequest.PageSize)
                : await _dataContext
                    .Set<T>()
                    .AsNoTracking()
                    .Where(filter)
                    .OrderBy(orderByExpression)
                    .ToPagedCollectionAsync(pagedRequest.PageIndex, pagedRequest.PageSize);
        }

        #endregion

        #region Implementación de interface IWriteableRepository

        async Task<T> IWriteableRepository<T>.InsertAsync(T entity)
        {
            using var transaction = await _dataContext.Database.BeginTransactionAsync();

            await _dataContext.AddAsync(entity);
            await _dataContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return entity;
        }

        async Task<T> IWriteableRepository<T>.UpdateAsync(T entity)
        {
            using var transaction = await _dataContext.Database.BeginTransactionAsync();

            _dataContext.Update(entity);
            await _dataContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return entity;
        }

        #endregion

        #region Implementación de interface IRemovableRepository

        async Task IRemovableRepository<T>.DeleteAsync(T entity)
        {
            using var transaction = await _dataContext.Database.BeginTransactionAsync();

            _dataContext.Remove(entity);
            await _dataContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        #endregion
    }
}