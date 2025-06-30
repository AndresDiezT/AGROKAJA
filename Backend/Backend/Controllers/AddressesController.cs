using Backend.DTOs.AddressDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses()
        {
            var result = await _addressService.GetAllAddressesAsync();

            return Ok(result.Data);
        }

        [HttpGet("by-user/{document}")]
        public async Task<ActionResult<Address>> GetAddressByUser(string document)
        {
            var result = await _addressService.GetAddressesByUserAsync(document);

            if (!result.Success)
            {
                if (result.Error == "Usuario no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressById(int id)
        {
            var result = await _addressService.GetAddressByIdAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Dirección no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(CreateAddressDto createAddressDto)
        {
            var result = await _addressService.CreateAddressAsync(createAddressDto);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            var address = result.Data;

            return CreatedAtAction(nameof(GetAddressById), new { id = address.IdAddress }, address);
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, UpdateAddressDto updateAddressDto)
        {
            if (id != updateAddressDto.IdAddress)
                return BadRequest(new { error = "El Id en la ruta no coincide con el del cuerpo" });

            var result = await _addressService.UpdateAddressAsync(updateAddressDto);

            if (!result.Success)
            {
                if (result.Error == "Dirección no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Addresses/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateAddress(int id)
        {
            var result = await _addressService.DeactivateAddressAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Dirección no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Addresses/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateAddress(int id)
        {
            var result = await _addressService.ActivateAddressAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Dirección no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }
    }
}