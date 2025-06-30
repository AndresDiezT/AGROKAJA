using Backend.DTOs.DepartmentDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departamentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departamentService = departmentService;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var result = await _departamentService.GetAllDepartmentsAsync();

            return Ok(result.Data);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var result = await _departamentService.GetDepartmentByIdAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Departamento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var result = await _departamentService.CreateDepartmentAsync(createDepartmentDto);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            var department = result.Data;

            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.IdDepartment }, department);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto)
        {
            if (id != updateDepartmentDto.IdCountry)
                return BadRequest(new { error = "El Id en la ruta no coincide con el del cuerpo" });

            var result = await _departamentService.UpdateDepartmentAsync(updateDepartmentDto);

            if (!result.Success)
            {
                if (result.Error == "Departamento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Departments/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateDepartment(int id)
        {
            var result = await _departamentService.DeactivateDepartmentAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Departamento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/Departments/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateDepartament(int id)
        {
            var result = await _departamentService.ActivateDepartmentAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Departamento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }
    }
}
