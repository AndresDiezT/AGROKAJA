using Backend.DTOs.ComissionDTOs;
using Backend.DTOs.CountryDTOs;
using Backend.Interfaces;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComissionsController : ControllerBase
    {
        private readonly IComissionService _comissionService;

        public ComissionsController(IComissionService comissionService)
        {
            _comissionService = comissionService;

        }

        // GET: api/Comissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comission>>> GetAllComissions()
        {
            var result = await _comissionService.GetAllComissionsAsync();

            return Ok(result);
        }

        // GET: api/Comissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comission>> GetComissionById([FromQuery]int id)
        {
            var result = await _comissionService.GetComissionByIdAsync(id);


            return Ok(result);
        }

        // POST: api/Comissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comission>> CreateComission([FromBody]CreateComissionDto dto)
        {
            var result = await _comissionService.CreateComissionAsync(dto);

            return Created("Comision subida exitosamente", result);
        }

        // PUT: api/Comissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComission([FromQuery]int id, [FromBody]CreateComissionDto dto)
        {

            var result = await _comissionService.UpdateComissionAsync(id,dto);

            return NoContent();
        }

        // PUT: api/Comissions/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateComission([FromQuery]int id)
        {
            var result = await _comissionService.DeactivateComissionAsync(id);

            return NoContent();
        }

        // PUT: api/Comissions/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateComission(int id)
        {
            var result = await _comissionService.ActivateComissionAsync(id);

            return NoContent();
        }

    }
}
