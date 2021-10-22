namespace Aurora.Framework.Platform.Identity
{
    /// <summary>
    /// Representa una clase que contiene la información de respuesta de inicio de sesión en Aurora Platform.
    /// </summary>
    public class IdentityAccess : PlatformBaseResponse
    {
        /// <summary>
        /// Token de acceso del proceso de inicio de sesión en Aurora Platform.
        /// </summary>
        public string AccessToken { get; set; }
    }
}