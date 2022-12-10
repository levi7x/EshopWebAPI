using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
