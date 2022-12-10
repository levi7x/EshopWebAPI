using AutoMapper;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using EshopWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
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


    }
}
