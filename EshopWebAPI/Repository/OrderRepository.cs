using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;

namespace EshopWebAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Order GetOrder(int orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
        }


        //Questionable ??
        public ICollection<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders.Where(o => o.User.Id == userId).ToList();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }
    }
}
