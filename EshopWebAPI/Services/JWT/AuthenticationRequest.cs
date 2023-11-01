using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Services.JWT
{
    public class AuthenticationRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
