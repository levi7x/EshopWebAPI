﻿using AutoMapper;
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
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, IProductRepository productRepository, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetOrders() 
        {
            _logger.LogInformation(DateTime.UtcNow + "Getting all orders");
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
            _logger.LogInformation(DateTime.UtcNow + $" Getting order with id {orderId}");
            var orders = _mapper.Map<OrderDto>(_orderRepository.GetOrder(orderId));

            if (orders == null)
            {
                _logger.LogWarning(DateTime.UtcNow + $"Category with id: {orderId} was not found");
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("cart/content")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCurrentCartContent()
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(currentUserId == null)
            {
                return BadRequest();
            }

            var getCartContent = _orderRepository.GetCurrentOrderDetails(currentUserId);

            if (getCartContent == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(getCartContent);
        }

        [HttpGet("cart/total")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetTotalCartAmount()
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(currentUserId == null)
            {
                return BadRequest();
            }

            var totalAmount = _orderRepository.GetTotalAmount(currentUserId);

            if (totalAmount == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(totalAmount);
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

        //add to cart -> relational

        [HttpPost("cart/{productId}")]
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

        [HttpDelete("cart/{productId}")]
        [Authorize]
        public IActionResult DeleteProductFromOrder(int productId, bool atOnce = false) 
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

            _orderRepository.RemoveProductFromOrder(product, activeOrder, atOnce);
            return NoContent();
        }

    }
}
