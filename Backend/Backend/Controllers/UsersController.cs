using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Interfaces;
using Backend.Services;
using Backend.DTOs.UserDTOs;
using Backend.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        // GET: api/Users/5
        [HttpGet("{document}")]
        public async Task<ActionResult<User>> GetUserByDocument(string document)
        {
            var user = await _userService.GetUserByDocumentAsync(document);

            if (user == null) return NotFound();

            return Ok(user);
        }

        // GET: api/Users/profile
        [HttpGet("profile")]
        public async Task<ActionResult<ReadUserDto>> GetUserByProfile()
        {
            var result = await _userService.GetProfileAsync();

            if (!result.Success)
            {
                if (result.Error == "Usuario no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string document, UpdateUserDto updateUserDto)
        {
            if (document != updateUserDto.Document) return BadRequest("El documento en la ruta no coincide con el del cuerpo");

            var updateUser = await _userService.UpdateUserAsync(updateUserDto);

            if (updateUser == null) return NotFound();

            return NoContent();
        }

        // PUT: api/Users/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(string document)
        {
            return await _userService.DeactivateUserAsync(document) ? NoContent() : BadRequest("El rol ya está invactivo o no existe");
        }

        // PUT: api/Users/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateUser(string document)
        {
            return await _userService.ActivateUserAsync(document) ? NoContent() : BadRequest("El rol ya está activo o no existe");
        }

        // GET: api/Users/filter
        // EJEMPLO: GET /api/user/filter?Username=juan&IsActive=true&Page=1&PageSize=5&SortBy=CreatedAt&SortDesc=true&SelectFields=Username&SelectFields=Email
        [HttpGet("filter")]
        public async Task<IActionResult> FilterUsers([FromQuery] UserFilterDto filterDto)
        {
            var result = await _userService.FilterUsersAsync(filterDto);
            return Ok(result);
        }
    }
}
