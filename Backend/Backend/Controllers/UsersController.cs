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
        [Authorize(Policy = "permission:admin.users.read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        // GET: api/Users/filter
        // EJEMPLO: GET /api/user/filter?Username=juan&IsActive=true&Page=1&PageSize=5&SortBy=CreatedAt&SortDesc=true&SelectFields=Username&SelectFields=Email
        [Authorize(Policy = "permission:admin.employees.read")]
        [HttpGet("filter/employees")]
        public async Task<IActionResult> GetEmployees([FromQuery] UserFilterDto filters)
        {
            var result = await _userService.FilterUsersAsync(filters, "employee");

            return Ok(result);
        }

        [Authorize(Policy = "permission:admin.customers.read")]
        [HttpGet("filter/customers")]
        public async Task<IActionResult> GetCustomers([FromQuery] UserFilterDto filters)
        {
            var result = await _userService.FilterUsersAsync(filters, "customer");

            return Ok(result);
        }

        // GET: api/Users/5
        [Authorize(Policy = "permission:admin.users.details")]
        [HttpGet("{document}")]
        public async Task<ActionResult<User>> GetUserByDocument(string document)
        {
            var user = await _userService.GetUserByDocumentAsync(document);

            if (user == null) return NotFound();

            return Ok(user);
        }

        // PUT: api/Users/123111
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "permission:common.profile.access")]
        [HttpPut("{document}")]
        public async Task<IActionResult> UpdateUser(string document, [FromBody]UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUserAsync(document, updateUserDto);

            if (result.Success)
                return Ok(result.Data);

            var errors = new Dictionary<string, string[]>();

            switch (result.Error)
            {

                default:
                    errors["General"] = new[] { result.Error };
                    break;
            }

            return BadRequest(new { errors });
        }

        // POST: api/Users/verify-email-confirm
        [HttpPost("verify-email-confirm")]
        public async Task<IActionResult> ConfirmEmailCode([FromBody] VerifyCodeDto dto)
        {
            var result = await _userService.VerifyEmailCodeAsync(dto.Document, dto.Code);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(new { message = "Código verificado" });
        }

        // PUT: api/Users/5/deactivate
        [Authorize(Policy = "permission:admin.users.active")]
        [HttpPut("{document}/deactivate")]
        public async Task<IActionResult> DeactivateUser(string document)
        {
            return await _userService.DeactivateUserAsync(document) ? NoContent() : BadRequest("El rol ya está invactivo o no existe");
        }

        // PUT: api/Users/5/activate
        [Authorize(Policy = "permission:admin.users.deactive")]
        [HttpPut("{document}/activate")]
        public async Task<IActionResult> ActivateUser(string document)
        {
            return await _userService.ActivateUserAsync(document) ? NoContent() : BadRequest("El rol ya está activo o no existe");
        }
    }
}
