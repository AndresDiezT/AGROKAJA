using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeDocumentsController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public TypeDocumentsController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/TypeDocuments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeDocument>>> GetTypesDocument()
        {
            return await _context.TypesDocument.ToListAsync();
        }

        // GET: api/TypeDocuments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDocument>> GetTypeDocument(int id)
        {
            var typeDocument = await _context.TypesDocument.FindAsync(id);

            if (typeDocument == null)
            {
                return NotFound();
            }

            return typeDocument;
        }

        // PUT: api/TypeDocuments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeDocument(int id, TypeDocument typeDocument)
        {
            if (id != typeDocument.IdTypeDocument)
            {
                return BadRequest();
            }

            _context.Entry(typeDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeDocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TypeDocuments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeDocument>> PostTypeDocument(TypeDocument typeDocument)
        {
            _context.TypesDocument.Add(typeDocument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeDocument", new { id = typeDocument.IdTypeDocument }, typeDocument);
        }

        // DELETE: api/TypeDocuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeDocument(int id)
        {
            var typeDocument = await _context.TypesDocument.FindAsync(id);
            if (typeDocument == null)
            {
                return NotFound();
            }

            _context.TypesDocument.Remove(typeDocument);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeDocumentExists(int id)
        {
            return _context.TypesDocument.Any(e => e.IdTypeDocument == id);
        }
    }
}
