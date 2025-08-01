using Backend.DTOs.ComissionDTOs;
using Backend.DTOs.OrderStatusDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;

        }

        // GET: api/Comissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetAllComissions()
        {
            var result = await _orderStatusService.GetAllOrderStatusAsync();

            return Ok(result);
        }

        // GET: api/Comissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatusById([FromQuery] int id)
        {
            var result = await _orderStatusService.GetOrderStatusByIdAsync(id);


            return Ok(result);
        }

        // POST: api/Comissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> CreateOrderStatus([FromBody] SaveOrderStatusDto dto)
        {
            var result = await _orderStatusService.CreateOrderStatusAsync(dto);

            return Created("estado de la orden subida exitosamente", result);
        }

        // PUT: api/Comissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus([FromQuery] int id, [FromBody] SaveOrderStatusDto dto)
        {

            var result = await _orderStatusService.UpdateOrderStatusAsync(id, dto);

            return NoContent();
        }

        // PUT: api/Comissions/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateOrderStatus([FromQuery] int id)
        {
            var result = await _orderStatusService.DeactivateOrderStatusAsync(id);

            return NoContent();
        }

        // PUT: api/Comissions/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateOrderStatus(int id)
        {
            var result = await _orderStatusService.ActivateOrderStatusAsync(id);

            return NoContent();
        }
    }
}