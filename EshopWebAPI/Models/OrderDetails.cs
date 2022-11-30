using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EshopWebAPI.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int Pieces { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set;}
    }
}
