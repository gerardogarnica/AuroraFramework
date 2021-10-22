using System;

namespace Aurora.Framework.Logic.Data
{
    /// <summary>
    /// Clase base de entidad auditable de modelo de datos.
    /// </summary>
    public abstract class AuditableDataEntity : IDataEntity
    {
        /// <summary>
        /// Nombre de usuario que crea el registro.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Fecha de creación del registro.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Nombre de usuario que generó la última actualización del registro.
        /// </summary>
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Fecha de última actualización del registro.
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }
    }

    /// <summary>
    /// Métodos de extensión para la clase AuditableDataEntity.
    /// </summary>
    public static class AuditableDataEntityExtensions
    {
        /// <summary>
        /// Agrega la información de creación de la entidad del modelo de datos.
        /// </summary>
        /// <param name="dataEntity">Entidad auditable del modelo de datos.</param>
        /// <param name="userName">Nombre de usuario.</param>
        public static void AddCreated(this AuditableDataEntity dataEntity, string userName)
        {
            dataEntity.CreatedBy = userName;
            dataEntity.CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Agrega la información de última actualización de la entidad del modelo de datos.
        /// </summary>
        /// <param name="dataEntity">Entidad auditable del modelo de datos.</param>
        /// <param name="userName">Nombre de usuario.</param>
        public static void AddLastUpdated(this AuditableDataEntity dataEntity, string userName)
        {
            dataEntity.LastUpdatedBy = userName;
            dataEntity.LastUpdatedDate = DateTime.Now;
        }
    }
}