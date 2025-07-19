using Backend.Data;
using Backend.DTOs.PresentationDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class PresentationService : IPresentationService
    {
        private readonly BackendDbContext _context;
        public PresentationService(BackendDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Presentation>> GetAllPresentationsAsync()
        {
            var presentations = await _context.Presentations.ToListAsync();
            return presentations;
        }
        public async Task<Presentation> GetPresentationByIdAsync(int idPresentation)
        {
            var presentation = await _context.Presentations.FindAsync(idPresentation);
            if (presentation == null)
            {
                throw new KeyNotFoundException($"La presentación con el ID {idPresentation} no existe.");
            }
            return presentation;
        }
        public async Task<Presentation> CreatePresentationAsync(CreatePresentationDto createPresentationDto)
        {
            var presentationExists = await _context.Presentations
                .AnyAsync(p => p.NamePresentation == createPresentationDto.NamePresentation);

            if (presentationExists)
            {
                throw new InvalidOperationException($"La presentación '{createPresentationDto.NamePresentation}' ya existe.");
            }

            // Generar abreviación eliminando vocales del nombre
            var abbreviation = GenerateAbbreviationWithoutVowels(createPresentationDto.NamePresentation);

            var presentation = new Presentation
            {
                NamePresentation = createPresentationDto.NamePresentation,
                AbbreviationPresentation = abbreviation,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Presentations.Add(presentation);
            await _context.SaveChangesAsync();
            return presentation;
        }

        // Método auxiliar
        private string GenerateAbbreviationWithoutVowels(string input)
        {
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            var abbreviation = new string(
                input.Where(c => char.IsLetter(c) && !vowels.Contains(c)).ToArray()
            );

            return abbreviation.ToUpper(); // Ejemplo: "Botella Grande" → "BTLLGRND"
        }
        public async Task<Presentation> UpdatePresentationAsync(int idPresentation, CreatePresentationDto updatePresentationDto)
        {
            var presentation = await _context.Presentations.FindAsync(idPresentation);
            if (presentation == null)
            {
                throw new KeyNotFoundException($"La presentación con el ID {idPresentation} no existe.");
            }
            var presentationExists = await _context.Presentations
                .AnyAsync(p => p.NamePresentation == updatePresentationDto.NamePresentation && p.IdPresentation != idPresentation);
            if (presentationExists)
            {
                throw new InvalidOperationException($"La presentación '{updatePresentationDto.NamePresentation}' ya existe.");
            }
            // Actualizar abreviación eliminando vocales del nombre
            var abbreviation = GenerateAbbreviationWithoutVowels(updatePresentationDto.NamePresentation);
            presentation.NamePresentation = updatePresentationDto.NamePresentation;
            presentation.AbbreviationPresentation = abbreviation;
            presentation.UpdatedAt = DateTime.UtcNow;
            _context.Presentations.Update(presentation);
            await _context.SaveChangesAsync();
            return presentation;
        }
        public async Task<bool> DeactivatePresentationAsync(int idPresentation)
        {
            var presentation = await _context.Presentations.FindAsync(idPresentation);
            if (presentation == null)
            {
                throw new KeyNotFoundException($"La presentación con el ID {idPresentation} no existe.");
            }
            if (!presentation.IsActive)
            {
                throw new InvalidOperationException($"La presentación con el ID {idPresentation} ya está desactivada.");
            }
            presentation.IsActive = false;
            presentation.DeactivatedAt = DateTime.UtcNow;
            _context.Presentations.Update(presentation);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActivatePresentationAsync(int idPresentation)
        {
            var presentation = await _context.Presentations.FindAsync(idPresentation);
            if (presentation == null)
            {
                throw new KeyNotFoundException($"La presentación con el ID {idPresentation} no existe.");
            }
            if (presentation.IsActive)
            {
                throw new InvalidOperationException($"La presentación con el ID {idPresentation} ya está activa.");
            }
            presentation.IsActive = true;
            presentation.DeactivatedAt = DateTime.MinValue; // Resetear la fecha de desactivación
            _context.Presentations.Update(presentation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
