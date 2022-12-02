using EshopWebAPI.Models;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IOrderRepository
    {
        public ICollection<Order> GetOrders();
        public Order GetOrder(int orderId);
        public ICollection<Order> GetOrdersByUser(string userId);
    }
}
