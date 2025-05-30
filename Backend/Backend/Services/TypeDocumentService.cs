using Backend.Data;
using Backend.DTOs.RoleDTOs;
using Backend.DTOs.TypeDocumentDto;
using Backend.DTOs.UserDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class TypeDocumentService : ITypeDocumentService
    {
        private readonly BackendDbContext _context;

        public TypeDocumentService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeDocument>> GetAllTypesDocumentAsync()
        {
            return await _context.TypesDocument
                .Include(u => u.Users)
                .ToListAsync();
        }

        public async Task<TypeDocument?> GetTypeDocumentByIdAsync(int id)
        {
            return await _context.TypesDocument
                .Include(u => u.Users)
                .FirstOrDefaultAsync(u => u.IdTypeDocument == id);
        }

        public async Task<TypeDocument> CreateTypeDocumentAsync(CreateTypeDocumentDto createTypeDocumentDto)
        {
            var newTypeDocument = new TypeDocument
            {
                NameTypeDocument = createTypeDocumentDto.NameTypeDocument,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.TypesDocument.Add(newTypeDocument);

            await _context.SaveChangesAsync();

            return newTypeDocument;
        }

        public async Task<TypeDocument?> UpdateTypeDocumentAsync(UpdateTypeDocumentDto updateTypeDocumentDto)
        {
            var existingTypeDocument = await _context.TypesDocument.FindAsync(updateTypeDocumentDto.IdTypeDocument);

            if (existingTypeDocument == null) return null;

            existingTypeDocument.NameTypeDocument = updateTypeDocumentDto.NameTypeDocument;
            existingTypeDocument.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingTypeDocument;
        }

        public async Task<bool> DeactivateTypeDocumentAsync(int id)
        {
            var typeDocument = await _context.TypesDocument.FindAsync(id);

            if (typeDocument == null) return false;

            typeDocument.IsActive = false;
            typeDocument.UpdatedAt = DateTime.Now;
            typeDocument.DeactivatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateTypeDocumentAsync(int id)
        {
            var typeDocument = await _context.TypesDocument.FindAsync(id);

            if (typeDocument == null) return false;

            typeDocument.IsActive = true;
            typeDocument.UpdatedAt = DateTime.Now;
            typeDocument.DeactivatedAt = null;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
