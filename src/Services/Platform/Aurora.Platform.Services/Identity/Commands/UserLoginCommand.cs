using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aurora.Platform.Services.Identity.Commands
{
    public class UserLoginCommand : IRequest<IdentityAccess>
    {
        [Required]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}