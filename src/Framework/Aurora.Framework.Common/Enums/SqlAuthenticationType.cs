using System;

namespace Aurora.Framework
{
    /// <summary>
    /// Tipo de autenticación a una base de datos SQL Server.
    /// </summary>
    [Serializable()]
    public enum SqlAuthenticationType
    {
        /// <summary>
        /// Autenticación de tipo SQL.
        /// </summary>
        SqlAuthentication,

        /// <summary>
        /// Autenticación de tipo Windows.
        /// </summary>
        WindowsAuthentication
    }
}