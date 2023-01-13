using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IOrderRepository
    {
        public ICollection<Order> GetOrders();
        public Order GetOrder(int orderId);
        public ICollection<Order> GetOrdersByUser(string userId);
        public bool CreateOrder(Order order);
        //public bool UpdateOrder(Order order);

        //relational
        public bool HasActiveOrder(string userId);
        public bool IncrementPieces(OrderDetails orderDetails);
        public bool IncrementPieces(OrderDetails orderDetails, bool increment);
        public double GetTotalAmount(string userId);
        public Order GetActiveOrder(string userId);
        public OrderDetails GetOrderDetail(Product product, Order order);
        public bool AddProductToOrder(Product product, Order order);
        public bool IsProductInOrder(Product product, Order order);
        public bool RemoveProductFromOrder(Product product, Order order, bool atOnce = false);
        //public CartDTOResponse GetCurrentOrderDetails(string userId);
        public ICollection<CartDTO> GetCurrentOrderDetails(string userId);
    }
}
