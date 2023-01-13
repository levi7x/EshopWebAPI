namespace EshopWebAPI.Models.Dto
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int Pieces { get; set; }

    }
}
