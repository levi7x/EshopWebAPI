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

        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool HasActiveOrder(string userId)
        {
            var orders = _context.Orders.Where(o => o.User.Id == userId && o.IsOrderActive == true).ToList();
            
            return orders.Any();
        }

        public bool AddProductToOrder(Product product, Order order)
        {

            var orderProduct = new OrderDetails()
            {
                Product = product,
                ProductId = product.Id,
                Order = order,
                OrderId = order.Id
            };

            if (IsProductInOrder(product, order)) 
            {
                return false;
            }

            _context.OrderDetails.Add(orderProduct);

            return Save();            
        }
        
        public Order GetActiveOrder(string userId)
        {
            var order = _context.Orders.Where(o => o.User.Id == userId && o.IsOrderActive == true).FirstOrDefault();
            return order;
        }

        public bool IsProductInOrder(Product product, Order order)
        {
            if (_context.OrderDetails.Where(od => od.OrderId == order.Id && od.ProductId == product.Id).Any())
            {
                return true;
            }
            return false;
        }

        public bool RemoveProductFromOrder(Product product, Order order)
        {
            var orderProduct = new OrderDetails()
            {
                Product = product,
                ProductId = product.Id,
                Order = order,
                OrderId = order.Id
            };

            if (IsProductInOrder(product, order))
            {
                _context.OrderDetails.Remove(orderProduct);
                return Save();
            }
            return false;
        }

        public bool IncrementPieces(OrderDetails orderDetails, bool increment)
        {
            var orderProduct = _context.OrderDetails.FirstOrDefault(od => od.ProductId == orderDetails.ProductId && od.OrderId == orderDetails.OrderId);
            if (increment) { orderDetails.Pieces++; }
            else { orderDetails.Pieces--; };

            _context.OrderDetails.Update(orderProduct);
            return Save();
        }

        public bool IncrementPieces(OrderDetails orderDetails)
        {
            throw new NotImplementedException();
        }
    }
}
