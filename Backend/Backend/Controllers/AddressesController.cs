using Backend.DTOs;
using Backend.DTOs.AddressDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddresses()
        {
            var result = await _addressService.GetAllAddressesAsync();

            return Ok(result.Data);
        }

        // GET: api/by-user/document
        [Authorize(Roles = "Admin")]
        [HttpGet("by-user/{document}")]
        public async Task<ActionResult<ReadAddressDto>> GetAddressByUser(string document)
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
        [Authorize(Policy = "permission:common.profile.access")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressById(int id, string userDocument)
        {
            var result = await _addressService.GetAddressByIdAsync(id, userDocument);

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
        [Authorize(Policy = "permission:common.profile.access")]
        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(CreateAddressDto createAddressDto)
        {
            var result = await _addressService.CreateAddressAsync(createAddressDto);

            if (result.Success)
                return Ok(result.Data);

            var errors = new Dictionary<string, string[]>();

            switch (result.Error)
            {
                case "Esta dirección ya esta registrada":
                    errors["General"] = new[] { result.Error };
                    break;

                default:
                    errors["General"] = new[] { result.Error };
                    break;
            }

            return BadRequest(new { errors });
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "permission:common.profile.access")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody]UpdateAddressDto updateAddressDto)
        {
            var result = await _addressService.UpdateAddressAsync(id, updateAddressDto);

            if (result.Success)
                return Ok(result.Data);

            var errors = new Dictionary<string, string[]>();

            switch (result.Error)
            {
                case "Esta dirección ya esta registrada":
                    errors["General"] = new[] { result.Error };
                    break;

                default:
                    errors["General"] = new[] { result.Error };
                    break;
            }

            return BadRequest(new { errors });
        }

        // PUT: api/Addresses/5/deactivate
        [Authorize(Policy = "permission:common.profile.access")]
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
        [Authorize(Policy = "permission:common.profile.access")]
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