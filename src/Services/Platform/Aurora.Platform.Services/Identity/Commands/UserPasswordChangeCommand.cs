using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Identity.Commands
{
    public class UserPasswordChangeCommand : IRequest<UserPasswordChangeResponse>
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string NewPassword { get; set; }
    }
}