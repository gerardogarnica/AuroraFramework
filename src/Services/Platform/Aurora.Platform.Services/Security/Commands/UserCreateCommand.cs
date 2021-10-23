using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Security.Commands
{
    public class UserCreateCommand : IRequest<UserResponse>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string LoginName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b",
            ErrorMessage = "La dirección de correo electrónico no cumple con el formato requerido.")]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}