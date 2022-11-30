using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Subscribed { get; set; } = false;
        public Address? Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
