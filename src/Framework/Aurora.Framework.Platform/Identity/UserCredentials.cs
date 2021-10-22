using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Identity
{
    /// <summary>
    /// Representa una clase que contiene la información de credenciales para inicio de sesión en Aurora Platform.
    /// </summary>
    public class UserCredentials
    {
        /// <summary>
        /// Nombre de inicio de sesión de usuario.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string LoginName { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        [Required(ErrorMessage = "La contraseña de usuario es obligatoria.")]
        public string Password { get; set; }
    }
}