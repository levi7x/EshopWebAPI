using System.ComponentModel.DataAnnotations;

namespace EshopWebAPI.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
    }
}
