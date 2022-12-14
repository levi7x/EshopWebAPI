using AutoMapper;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using EshopWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrders() 
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetOrders());
            return Ok(orders);
        }

        [HttpGet("{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrder(int orderId)
        {
            if (orderId == 0)
            {
                return BadRequest();
            }

            var orders = _mapper.Map<OrderDto>(_orderRepository.GetOrder(orderId));

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateOrder()
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_orderRepository.HasActiveOrder(currentUserId))
            {
                return Ok();
            }

            var user = _userRepository.GetUser(currentUserId);
            
            if (user == null)
            {
                return BadRequest();
            }

            Order createdOrder = new Order()
            {
                CreatedOrderDate = DateTime.Now,
                Status = Data.Enum.OrderStatus.Pending,
                IsOrderActive = true,
                User = user
            };

            _orderRepository.CreateOrder(createdOrder);

            var orderDto = new OrderDto()
            {
                CreatedOrderDate = createdOrder.CreatedOrderDate,
                Status = createdOrder.Status
            };

            return Ok(orderDto);
        }

        //add to cart

        [HttpPost("/cart/{productId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddProductToOrder(int productId)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!_orderRepository.HasActiveOrder(currentUserId))
            {
                return NotFound();
            }

            var activeOrder = _orderRepository.GetActiveOrder(currentUserId);

            var product = _productRepository.GetProduct(productId);

            if (product == null)
            {
                return BadRequest();
            }

            var result = _orderRepository.AddProductToOrder(product, activeOrder);

            return Ok(result);
        }

        [HttpDelete("/cart/delete")]
        [Authorize]
        public IActionResult DeleteProductFromOrder(int productId) 
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!_orderRepository.HasActiveOrder(currentUserId))
            {
                return NotFound();
            }

            var activeOrder = _orderRepository.GetActiveOrder(currentUserId);

            var product = _productRepository.GetProduct(productId);

            if (product == null)
            {
                return BadRequest();
            }

            _orderRepository.RemoveProductFromOrder(product, activeOrder);
            return NoContent();
        }

    }
}
