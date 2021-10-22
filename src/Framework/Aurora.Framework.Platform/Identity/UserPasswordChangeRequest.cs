using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Identity
{
    /// <summary>
    /// Representa una clase que contiene la información para la modificación de contraseña de un usuario.
    /// </summary>
    public class UserPasswordChangeRequest
    {
        /// <summary>
        /// Contraseña actual del usuario.
        /// </summary>
        [Required(ErrorMessage = "La contraseña actual es obligatoria.")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Nueva contraseña del usuario.
        /// </summary>
        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La longitud mínima de la nueva contraseña es de 8 caracteres.")]
        [MaxLength(25, ErrorMessage = "La longitud máxima de la nueva contraseña es de 25 caracteres.")]
        public string NewPassword { get; set; }
    }
}