using Backend.DTOs.DepartmentDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "permission:common.departments.read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var result = await _departamentService.GetAllDepartmentsAsync();

            return Ok(result.Data);
        }

        // GET: api/Departments/filter
        [Authorize(Policy = "permission:admin.departments.read")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterDepartments([FromQuery] DepartmentFilterDto filterDto)
        {
            var result = await _departamentService.FilterDepartmentsAsync(filterDto);
            return Ok(result);
        }

        // GET: api/Departments/5
        [Authorize(Policy = "permission:admin.departments.details")]
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
        [Authorize(Policy = "permission:admin.departments.create")]
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
        [Authorize(Policy = "permission:admin.departments.update")]
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
        [Authorize(Policy = "permission:admin.departments.deactive")]
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
        [Authorize(Policy = "permission:admin.departments.active")]
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
