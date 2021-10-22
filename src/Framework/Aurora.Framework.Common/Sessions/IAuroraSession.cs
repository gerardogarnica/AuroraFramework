namespace Aurora.Framework.Sessions
{
    /// <summary>
    /// Interface de administración de sesiones de usuario de Aurora.
    /// </summary>
    public interface IAuroraSession
    {
        /// <summary>
        /// Obtiene la información de sesión de un usuario en caso de que se haya iniciado sesión.
        /// </summary>
        /// <returns>Información de sesión de un usuario.</returns>
        AuroraSessionInfo GetSessionInfo();
    }
}