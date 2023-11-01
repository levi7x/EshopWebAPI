using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenCreated { get; set; } = DateTime.Now;
        public DateTime? TokenExpires { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Subscribed { get; set; } = false;
        public Address? Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
