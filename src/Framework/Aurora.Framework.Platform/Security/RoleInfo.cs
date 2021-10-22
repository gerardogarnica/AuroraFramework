using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Framework.Platform.Security
{
    /// <summary>
    /// Representa una clase que contiene la información de un rol de usuarios en Aurora Platform.
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Identificador del repositorio.
        /// </summary>
        public int RepositoryId { get; set; }

        /// <summary>
        /// Nombre único del rol.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descripción general del rol.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indica si es un rol por defecto de la plataforma.
        /// </summary>
        public bool IsDefaultRole { get; set; }

        /// <summary>
        /// Indica si el registro de rol se encuentra activo en la plataforma.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Representa una clase que contiene la información para la creación de un rol de usuario en Aurora Platform.
        /// </summary>
        public class CreateRequest
        {
            /// <summary>
            /// Identificador único del repositorio.
            /// </summary>
            [Required]
            public int RepositoryId { get; set; }

            /// <summary>
            /// Nombre descriptivo del rol de usuario.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string Name { get; set; }

            /// <summary>
            /// Descripción completa del rol de usuario.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string Description { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información para la actualización de un rol de usuario en Aurora Platform.
        /// </summary>
        public class UpdateRequest
        {
            /// <summary>
            /// Identificador único del rol de usuario.
            /// </summary>
            [Required]
            public int RoleId { get; set; }

            /// <summary>
            /// Descripción completa del rol de usuario.
            /// </summary>
            [Required]
            [MinLength(3)]
            [MaxLength(100)]
            public string Description { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información para agregar o eliminar usuarios de un rol registrado en Aurora Platform.
        /// </summary>
        public class SaveUsersRequest
        {
            /// <summary>
            /// Identificador único del rol de usuario.
            /// </summary>
            public int RoleId { get; set; }

            /// <summary>
            /// Lista de identificadores únicos de usuarios a agregar al rol.
            /// </summary>
            public IList<int> UsersToAdd { get; set; }

            /// <summary>
            /// Lista de identificadores únicos de usuarios a eliminar del rol.
            /// </summary>
            public IList<int> UsersToRemove { get; set; }
        }

        /// <summary>
        /// Representa una clase que contiene la información de respuesta de operaciones de roles de usuarios de Aurora Platform.
        /// </summary>
        public class Response : PlatformBaseResponse
        {
            /// <summary>
            /// Identificador único del rol de usuario.
            /// </summary>
            public int RoleId { get; private set; }

            /// <summary>
            /// Nombre descriptivo del rol de usuario.
            /// </summary>
            public string Name { get; private set; }
        }
    }
}