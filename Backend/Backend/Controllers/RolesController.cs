using Backend.DTOs;
using Backend.DTOs.CountryDTOs;
using Backend.DTOs.RoleDTOs;
using Backend.Interfaces;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Authorize(Policy = "permission:common.roles.read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            return Ok(await _roleService.GetAllRolesAsync());
        }

        // GET: api/Roles/filter
        [Authorize(Policy = "permission:admin.roles.read")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterRoles([FromQuery] RoleFilterDto filterDto)
        {
            var result = await _roleService.FilterRolesAsync(filterDto);
            return Ok(result);
        }

        // GET: api/Roles/5
        [Authorize(Policy = "permission:admin.roles.details")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);

            return role != null ? Ok(role) : NotFound();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "permission:admin.roles.create")]
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(CreateRoleDto createRoleDto)
        {
            var result = await _roleService.CreateRoleAsync(createRoleDto);

            if (result.Success)
                return Ok(result.Data);

            var errors = new Dictionary<string, string[]>();

            switch (result.Error)
            {
                case "Ya existe un rol con este nombre":
                    errors["nameRole"] = new[] { result.Error };
                    break;

                default:
                    errors["General"] = new[] { result.Error };
                    break;
            }

            return BadRequest(new { errors });
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "permission:admin.roles.update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, UpdateRoleDto updateRoleDto)
        {
            if (id != updateRoleDto.IdRole) return BadRequest();

            var updateRole = await _roleService.UpdateRoleAsync(updateRoleDto);

            if (updateRole == null) return NotFound();

            return NoContent();
        }

        // PUT: api/Roles/5/deactivate
        [Authorize(Policy = "permission:admin.roles.deactive")]
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateRole(int id)
        {
            return await _roleService.DeactivateRoleAsync(id) ? NoContent() : BadRequest("El rol ya está invactivo o no existe");
        }

        // PUT: api/Roles/5/activate
        [Authorize(Policy = "permission:admin.roles.active")]
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateRole(int id)
        {
            return await _roleService.ActivateRoleAsync(id) ? NoContent() : BadRequest("El rol ya está activo o no existe");
        }
    }
}
