using Backend.DTOs.CountryDTOs;
using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: api/Countries
        [Authorize(Policy = "permission:common.countries.read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadCountryDto>>> GetAllCountries()
        {
            var result = await _countryService.GetAllCountriesAsync();

            return Ok(result.Data);
        }

        // GET: api/Countries/filter
        [Authorize(Policy = "permission:admin.countries.read")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterCountries([FromQuery] CountryFilterDto filterDto)
        {
            var result = await _countryService.FilterCountriesAsync(filterDto);
            return Ok(result);
        }


        // GET: api/Countries/5
        [Authorize(Policy = "permission:admin.countries.details")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryById(int id)
        {
            var result = await _countryService.GetCountryByIdAsync(id);

            if (!result.Success)
            {
                if (result.Error == "País no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "permission:admin.countries.create")]
        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(CreateCountryDto createCountryDto)
        {
            var result = await _countryService.CreateCountryAsync(createCountryDto);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            var country = result.Data;

            return CreatedAtAction(nameof(GetCountryById), new { id = country.IdCountry }, country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "permission:admin.countries.update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.IdCountry)
                return BadRequest(new { error = "El Id en la ruta no coincide con el del cuerpo" });

            var result = await _countryService.UpdateCountryAsync(updateCountryDto);

            if (!result.Success)
            {
                if (result.Error == "País no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Countries/5/deactivate
        [Authorize(Policy = "permission:admin.countries.deactive")]
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateCountry(int id)
        {
            var result = await _countryService.DeactivateCountryAsync(id);

            if (!result.Success)
            {
                if (result.Error == "País no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Countries/5/activate
        [Authorize(Policy = "permission:admin.countries.active")]
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateCountry(int id)
        {
            var result = await _countryService.ActivateCountryAsync(id);

            if (!result.Success)
            {
                if (result.Error == "País no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }
    }
}
