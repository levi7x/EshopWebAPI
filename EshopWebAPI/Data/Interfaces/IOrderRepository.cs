using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IOrderRepository
    {
        public ICollection<Order> GetOrders();
        public Order GetOrder(int orderId);
        public ICollection<Order> GetOrdersByUser(string userId);
        public bool CreateOrder(Order order);
        public bool HasActiveOrder(string userId);
        public bool IncrementPieces(OrderDetails orderDetails);
        public bool IncrementPieces(OrderDetails orderDetails, bool increment);
        public Order GetActiveOrder(string userId);
        public bool AddProductToOrder(Product product, Order order);
        public bool IsProductInOrder(Product product, Order order);
        public bool RemoveProductFromOrder(Product product, Order order);
    }
}
