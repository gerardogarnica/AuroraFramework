namespace Aurora.Framework.Sessions
{
    /// <summary>
    /// Representa una clase que contiene información de sesión de un usuario.
    /// </summary>
    public class AuroraSessionInfo
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Nombre de inicio de sesión del usuario.
        /// </summary>
        public string UserLoginName { get; set; }

        /// <summary>
        /// Nombre descriptivo del usuario.
        /// </summary>
        public string UserDescription { get; set; }
    }
}