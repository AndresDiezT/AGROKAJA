using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Interfaces;
using Backend.DTOs.RoleDTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            return role != null ? Ok(role) : NotFound();
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, UpdateRoleDto updateRoleDto)
        {
            if (id != updateRoleDto.IdRole) return BadRequest();

            var updateRole = await _roleService.UpdateRoleAsync(updateRoleDto);

            if (updateRole == null) return NotFound();

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(CreateRoleDto createRoleDto)
        {
            var role = await _roleService.CreateRoleAsync(createRoleDto);

            return CreatedAtAction(nameof(GetRoleById), new { id = role.IdRole }, role);
        }

        // PUT: api/Roles/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateRole(int id)
        {
            return await _roleService.DeactivateRoleAsync(id) ? NoContent() : BadRequest("El rol ya está invactivo o no existe");
        }

        // PUT: api/Roles/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateRole(int id)
        {
            return await _roleService.ActivateRoleAsync(id) ? NoContent() : BadRequest("El rol ya está activo o no existe");
        }
    }
}
