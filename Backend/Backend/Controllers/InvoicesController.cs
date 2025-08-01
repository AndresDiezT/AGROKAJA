using Backend.DTOs.ComissionDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _InvoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _InvoiceService = invoiceService;

        }

        //GET: api/Invoices/6
        [HttpGet("{invoiceNumber}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByIdAsync([FromQuery] string invoiceNumber)
        {
            var result = await _InvoiceService.GetInvoiceByNumberAsync(invoiceNumber);


            return Ok(result);
        }
        //GET: api/Invoices/10007344
        [HttpGet("By-User/{UserDocument}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByUserAsync([FromQuery] string UserDocument)
        {
            var result = await _InvoiceService.GetInvoiceByUserAsync(UserDocument);


            return Ok(result);
        }
    }
}
