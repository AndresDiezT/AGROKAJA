using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.DTOs.ComissionDTOs;
using Backend.Interfaces;
using Backend.DTOs.OrderDTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;

        }

        // GET: api/Comissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();

            return Ok(result);
        }

        // GET: api/Comissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById([FromQuery] int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);


            return Ok(result);
        }

        // POST: api/Comissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var result = await _orderService.CreateOrderAsync(dto);

            return Created("Orden subida exitosamente", result);
        }

        // PUT: api/Comissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromQuery] int id, [FromBody] UpdateOrderDto dto)
        {

            var result = await _orderService.UpdateOrderAsync(id, dto);

            return NoContent();
        }

        // PUT: api/Comissions/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateOrder([FromQuery] int id)
        {
            var result = await _orderService.DeactivateOrderAsync(id);

            return NoContent();
        }

        // PUT: api/Comissions/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateOrder(int id)
        {
            var result = await _orderService.ActivateOrderAsync(id);

            return NoContent();
        }

    }
}

