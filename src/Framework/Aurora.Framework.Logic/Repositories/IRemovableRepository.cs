using Aurora.Framework.Logic.Data;
using System.Threading.Tasks;

namespace Aurora.Framework.Logic.Repositories
{
    /// <summary>
    /// Interface de ejecución de operaciones de eliminación en repositorios de datos.
    /// </summary>
    public interface IRemovableRepository<T> where T : class, IDataEntity
    {
        /// <summary>
        /// Elimina un registro de tipo <typeparamref name="T"/> en el repositorio de datos.
        /// </summary>
        /// <param name="entity">Registro a ser eliminado en el repositorio.</param>
        Task DeleteAsync(T entity);
    }
}