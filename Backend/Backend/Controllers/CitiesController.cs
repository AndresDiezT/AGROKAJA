using Backend.DTOs.CityDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAllCities()
        {
            var result = await _cityService.GetAllCitiesAsync();

            return Ok(result.Data);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            var result = await _cityService.GetCityByIdAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Ciudad no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(CreateCityDto createCityDto)
        {
            var result = await _cityService.CreateCityAsync(createCityDto);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            var city = result.Data;

            return CreatedAtAction(nameof(GetCityById), new { id = city.IdCity }, city);
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, UpdateCityDto updateCityDto)
        {
            if (id != updateCityDto.IdCity)
                return BadRequest(new { error = "El Id en la ruta no coincide con el del cuerpo" });

            var result = await _cityService.UpdateCityAsync(updateCityDto);

            if (!result.Success)
            {
                if (result.Error == "Ciudad no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Cities/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateCity(int id)
        {
            var result = await _cityService.DeactivateCityAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Ciudad no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Cities/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateCity(int id)
        {
            var result = await _cityService.ActivateCityAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Ciudad no encontrada")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }
    }
}
