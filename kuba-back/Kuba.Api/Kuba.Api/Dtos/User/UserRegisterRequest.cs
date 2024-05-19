using System.ComponentModel.DataAnnotations;

namespace Kuba.Api.Dtos.User
{
    public class UserRegisterRequest
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The passwords are not the same")]
        public string ConfirmPassword { get; set; }
    }
}
