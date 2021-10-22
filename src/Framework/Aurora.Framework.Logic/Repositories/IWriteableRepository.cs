using Aurora.Framework.Logic.Data;
using System.Threading.Tasks;

namespace Aurora.Framework.Logic.Repositories
{
    /// <summary>
    /// Interface de ejecución de operaciones de escritura en repositorios de datos.
    /// </summary>
    public interface IWriteableRepository<T> where T : class, IDataEntity
    {
        /// <summary>
        /// Crea un registro de tipo <typeparamref name="T"/> en el repositorio de datos.
        /// </summary>
        /// <param name="entity">Registro a ser creado en el repositorio.</param>
        /// <returns>Registro creado en el repositorio.</returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Actualiza un registro de tipo <typeparamref name="T"/> en el repositorio de datos.
        /// </summary>
        /// <param name="entity">Registro a ser actualizado en el repositorio.</param>
        /// <returns>Registro actualizado en el repositorio.</returns>
        Task<T> UpdateAsync(T entity);
    }
}