
namespace EshopWebAPI.Models.Dto
{
    public class CartDTOResponse
    {
        public ICollection<CartDTO> CartContent { get; set; }
        public double TotalAmount { get; set; }
    }
}
