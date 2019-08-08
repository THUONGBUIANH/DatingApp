using System.ComponentModel.DataAnnotations;

namespace DatingApp_BE.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}