using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.TypeDocumentDto;
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

        public async Task<Result<IEnumerable<TypeDocument>>> GetAllTypesDocumentAsync()
        {
            var typesDocument = await _context.TypesDocument.ToListAsync();

            return Result<IEnumerable<TypeDocument>>.Ok(typesDocument);
        }

        public async Task<Result<TypeDocument>> GetTypeDocumentByIdAsync(int id)
        {
            var typeDocument = await _context.TypesDocument.FindAsync(id);

            if (typeDocument is null)
                return Result<TypeDocument>.Fail("Tipo de documento no encontrado");

            return Result<TypeDocument>.Ok(typeDocument);
        }

        public async Task<Result<TypeDocument>> CreateTypeDocumentAsync(CreateTypeDocumentDto createTypeDocumentDto)
        {
            if (await _context.TypesDocument.AnyAsync(u => u.NameTypeDocument == createTypeDocumentDto.NameTypeDocument))
                return Result<TypeDocument>.Fail("El tipo de documento ya existe");

            var newTypeDocument = new TypeDocument
            {
                NameTypeDocument = createTypeDocumentDto.NameTypeDocument,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.TypesDocument.Add(newTypeDocument);

            await _context.SaveChangesAsync();

            return Result<TypeDocument>.Ok(newTypeDocument);
        }

        public async Task<Result<TypeDocument>> UpdateTypeDocumentAsync(UpdateTypeDocumentDto updateTypeDocumentDto)
        {
            var existingTypeDocument = await _context.TypesDocument.FindAsync(updateTypeDocumentDto.IdTypeDocument);

            if (existingTypeDocument == null)
                Result<TypeDocument>.Fail("Tipo de documento no encontrado");

            existingTypeDocument.NameTypeDocument = updateTypeDocumentDto.NameTypeDocument;
            existingTypeDocument.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Result<TypeDocument>.Ok(existingTypeDocument);
        }

        public async Task<Result<bool>> DeactivateTypeDocumentAsync(int id)
        {
            var typeDocument = await _context.TypesDocument.FindAsync(id);

            if (typeDocument is null)
                return Result<bool>.Fail("Tipo de documento no encontrado");

            if (!typeDocument.IsActive)
                return Result<bool>.Fail("El tipo de documento ya esta inactivo");

            typeDocument.IsActive = false;
            typeDocument.UpdatedAt = DateTime.Now;
            typeDocument.DeactivatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ActivateTypeDocumentAsync(int id)
        {
            var typeDocument = await _context.TypesDocument.FindAsync(id);

            if (typeDocument is null)
                return Result<bool>.Fail("Tipo de documento no encontrado");

            if (typeDocument.IsActive)
                return Result<bool>.Fail("El tipo de documento ya esta activo");

            typeDocument.IsActive = true;
            typeDocument.UpdatedAt = DateTime.Now;
            typeDocument.DeactivatedAt = null;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
