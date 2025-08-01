using Backend.DTOs.PaymentStatusDTOs;
using Backend.Interfaces;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentStatusController : ControllerBase
    {
        private readonly IPaymentStatusService _paymentStatusService;

        public PaymentStatusController(IPaymentStatusService paymentStatusService)
        {
            _paymentStatusService = paymentStatusService;
        }

        // GET: api/PaymentStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentStatus>>> GetAllAsync()
        {
            var result = await _paymentStatusService.GetAllPaymentStatusAsync();
            return Ok(result);
        }

        // GET: api/PaymentStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentStatus>> GetByIdAsync(int id)
        {
            var result = await _paymentStatusService.GetPaymentStatusByIdAsync(id);
            return Ok(result);
        }

        // POST: api/PaymentStatus
        [HttpPost]
        public async Task<ActionResult<PaymentStatus>> CreateAsync([FromBody] SavePaymentStatusDto dto)
        {
            var created = await _paymentStatusService.CreatePaymentStatusAsync(dto);
            return Created("Estado de pago creado", created);
        }

        // PUT: api/PaymentStatus/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentStatus>> UpdateAsync(int id, [FromBody] SavePaymentStatusDto dto)
        {
            var updated = await _paymentStatusService.UpdatePaymentStatusAsync(id, dto);
            return Ok(updated);
        }

        // PUT: api/PaymentStatus/deactivate/5
        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateAsync(int id)
        {
            var result = await _paymentStatusService.DeactivatePaymentStatusAsync(id);
            return result ? Ok() : BadRequest("Ya está desactivado o no se pudo desactivar.");
        }

        // PUT: api/PaymentStatus/activate/5
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateAsync(int id)
        {
            var result = await _paymentStatusService.ActivatePaymentStatusAsync(id);
            return result ? Ok() : BadRequest("Ya está activo o no se pudo activar.");
        }
    }

}
