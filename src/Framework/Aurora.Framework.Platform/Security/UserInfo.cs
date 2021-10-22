using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Security
{
    /// <summary>
    /// Representa una clase que contiene la información de un usuario registrado en Aurora Platform.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Nombre de inicio de sesión único del usuario.
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// Descripción completa del usuario.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Indica si la contraseña del usuario debe ser modificado.
        /// </summary>
        public bool PasswordMustChange { get; set; }

        /// <summary>
        /// Fecha de expiración de la contraseña, en caso de aplicar.
        /// </summary>
        public DateTime? PasswordExpirationDate { get; set; }

        /// <summary>
        /// Indica si es un usuario por defecto de la plataforma.
        /// </summary>
        public bool IsDefaultUser { get; set; }

        /// <summary>
        /// Indica si el registro de usuario se encuentra activo en la plataforma.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Lista de roles a los que pertenece el usuario.
        /// </summary>
        public IList<RoleInfo> Roles { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de un usuario registrado en Aurora Platform.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Nombre de inicio de sesión único del usuario.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(35)]
            public string LoginName { get; set; }

            /// <summary>
            /// Descripción completa del usuario.
            /// </summary>
            [Required]
            [MaxLength(100)]
            public string Description { get; set; }

            /// <summary>
            /// Dirección de correo electrónico del usuario.
            /// </summary>
            [Required]
            [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b",
                ErrorMessage = "La dirección de correo electrónico no cumple con el formato requerido.")]
            [MaxLength(100)]
            public string Email { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información para agregar o eliminar roles de un usuario registrado en Aurora Platform.
        /// </summary>
        public class SaveRolesRequest
        {
            /// <summary>
            /// Nombre de inicio de sesión único del usuario.
            /// </summary>
            public string LoginName { get; set; }

            /// <summary>
            /// Lista de identificadores únicos de roles a agregar al usuario.
            /// </summary>
            public IList<int> RolesToAdd { get; set; }

            /// <summary>
            /// Lista de identificadores únicos de roles a eliminar del usuario.
            /// </summary>
            public IList<int> RolesToRemove { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de usuario de Aurora Platform.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del usuario.
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// Nombre de inicio de sesión único del usuario.
            /// </summary>
            public string LoginName { get; set; }
        }
    }
}