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
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByDocument(string document)
        {
            var user = await _userService.GetUserByDocumentAsync(document);

            if (user == null) return NotFound();

            return Ok(user);
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto createUserDto)
        {
            var user = await _userService.CreateUserAsync(createUserDto);

            return CreatedAtAction(nameof(GetUserByDocument), new { document = user.Document }, user);
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
    }
}
