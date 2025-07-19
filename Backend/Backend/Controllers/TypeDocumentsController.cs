using Backend.Data;
using Backend.DTOs.CountryDTOs;
using Backend.DTOs.TypeDocumentDto;
using Backend.Interfaces;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeDocumentsController : ControllerBase
    {
        private readonly ITypeDocumentService _typeDocumentService;

        public TypeDocumentsController(ITypeDocumentService typeDocumentService)
        {
            _typeDocumentService = typeDocumentService;
        }

        // GET: api/TypeDocuments
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeDocument>>> GetAllTypesDocument()
        {
            var result = await _typeDocumentService.GetAllTypesDocumentAsync();

            return Ok(result.Data);
        }

        // GET: api/TypeDocuments/5
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDocument>> GetTypeDocument(int id)
        {
            var result = await _typeDocumentService.GetTypeDocumentByIdAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Tipo de documento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Data);
        }

        // POST: api/TypeDocuments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeDocument>> CreateTypeDocument([FromBody]CreateTypeDocumentDto createTypeDocumentDto)
        {
            var result = await _typeDocumentService.CreateTypeDocumentAsync(createTypeDocumentDto);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            var typeDocument = result.Data;

            return CreatedAtAction(nameof(GetTypeDocument), new { id = typeDocument.IdTypeDocument }, typeDocument);
        }

        // PUT: api/TypeDocuments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTypeDocument(int id, UpdateTypeDocumentDto updateTypeDocumentDto)
        {
            if (id != updateTypeDocumentDto.IdTypeDocument)
                return BadRequest(new { error = "El Id en la ruta no coincide con el del cuerpo" });

            var result = await _typeDocumentService.UpdateTypeDocumentAsync(updateTypeDocumentDto);

            if (!result.Success)
            {
                if (result.Error == "Tipo de documento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/TypeDocuments/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateTypeDocument(int id)
        {
            var result = await _typeDocumentService.DeactivateTypeDocumentAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Tipo de documento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }

        // PUT: api/TypeDocuments/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateTypeDocument(int id)
        {
            var result = await _typeDocumentService.ActivateTypeDocumentAsync(id);

            if (!result.Success)
            {
                if (result.Error == "Tipo de documento no encontrado")
                    return NotFound(new { error = result.Error });

                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }
    }
}
