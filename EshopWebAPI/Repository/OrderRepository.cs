using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;

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

        public OrderDetails GetOrderDetail(Product product, Order order)
        {
            return _context.OrderDetails.FirstOrDefault(od => od.ProductId == product.Id && od.OrderId == order.Id);
        }

        public bool AddProductToOrder(Product product, Order order)
        {

            var orderProduct = new OrderDetails()
            {
                Product = product,
                ProductId = product.Id,
                Order = order,
                OrderId = order.Id,
            };

            if (IsProductInOrder(product, order)) 
            {
                var orderDetail = GetOrderDetail(product, order);

                orderDetail.Pieces++;

                _context.OrderDetails.Update(orderDetail);
                return Save();
            }

            orderProduct.Pieces = 1;
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

        public bool RemoveProductFromOrder(Product product, Order order, bool atOnce = false)
        {
            var orderDetail = GetOrderDetail(product, order);

            if (IsProductInOrder(product, order))
            {
                if(orderDetail.Pieces == 1 || atOnce == true)
                {
                    _context.OrderDetails.Remove(orderDetail);
                    return Save();
                }
                

                orderDetail.Pieces--;

                _context.OrderDetails.Update(orderDetail);
                return Save();
            }
            return false;
        }

        public bool IncrementPieces(OrderDetails orderDetails, bool increment)
        {
            throw new NotImplementedException();
        }

        public bool IncrementPieces(OrderDetails orderDetails)
        {
            throw new NotImplementedException();
        }






        /*
            SELECT Products.Id, Products.ProductName, OrderDetails.Pieces
            FROM OrderDetails
            JOIN Products ON Products.Id = OrderDetails.ProductId
            WHERE OrderDetails.OrderId = activeOrder;
        */

        public ICollection<CartDTO> GetCurrentOrderDetails(string userId)
        {
            var activeOrder = this.GetActiveOrder(userId);

            if(activeOrder == null)
            {
                //may happen after seed data
                throw new Exception("NENI BUG LEN SI NEPRIDAL ORDER K USEROVI");
            }

            //var currentOrderDetails = _context.OrderDetails.Where(o => o.OrderId == activeOrder.Id).Select(content => new {Product = content.Product,pieces = content.Pieces}).ToList();
            var currentOrderDetails = _context.OrderDetails.Where(o => o.OrderId == activeOrder.Id).Select(od => new {Product = od.Product,Pieces = od.Pieces}).ToList();
            List<CartDTO> cartContent = new List<CartDTO>();


            foreach (var item in currentOrderDetails)
            {
                var cart = new CartDTO()
                {
                    Id = item.Product.Id,
                    ProductName = item.Product.ProductName,
                    ProductImageUrl = item.Product.ProductImageUrl,
                    Price = item.Product.Price,
                    Stock = item.Product.Stock,
                    Pieces = item.Pieces
                };

                cartContent.Add(cart);
            }

            return cartContent;
        }

        /*
            SELECT SUM(p.price * od.pieces) as total_amount
            FROM products p
            JOIN orderdetails od ON p.id = od.productid
            WHERE od.orderid = current user Id
        */

        public double GetTotalAmount(string userId)
        {
            var orderId = GetActiveOrder(userId).Id;
            var totalAmount = _context.OrderDetails.Where(od => od.OrderId == orderId).Join(_context.Products, od => od.ProductId, p => p.Id, (od, p) => new { od.Pieces, p.Price }).Sum(x => x.Price * x.Pieces);
            return totalAmount;
        }
    }
}
