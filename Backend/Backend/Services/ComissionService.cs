using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.ComissionDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Backend.Services
{
    public class ComissionService : IComissionService
    {

        private readonly BackendDbContext _context;

        public ComissionService(BackendDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Comission>> GetAllComissionsAsync()
        {
            var comissions = await _context.Comissions.ToListAsync();

            return comissions;
        }
        public async Task<Comission> GetComissionByIdAsync(int id)
        {
            var comission = await _context.Comissions
                .FirstOrDefaultAsync(c => c.IdComission == id) ;
            if (comission is null)
            {
                throw new Exception("comision no encontrada");
            }
            return comission;
        }

        public async Task<Comission> CreateComissionAsync(CreateComissionDto dto)
        {
            if (await _context.Comissions.AnyAsync(c => c.NameComission == dto.NameComission))
                throw new Exception("comision ya registrada");

            var newComission = new Comission
            {
                NameComission = dto.NameComission,
                RateComission = dto.RateComission,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Comissions.Add(newComission);

            await _context.SaveChangesAsync();

            return newComission;
        }
        public async Task<Comission> UpdateComissionAsync(int id, CreateComissionDto dto)
        {
            var existingComission = await _context.Comissions.FindAsync(id);

            if (existingComission is null)
                throw new Exception("comision no encontrada");

            existingComission.NameComission = dto.NameComission;
            existingComission.RateComission = dto.RateComission;
            existingComission.UpdatedAt = DateTime.Now;

            _context.Entry(existingComission).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return existingComission;
        }

        public async Task<bool> DeactivateComissionAsync(int id)
        {
            var comission = await _context.Comissions.FindAsync(id);

            if (comission is null)
                throw new Exception("comision no encontrada");

            if (!comission.IsActive)
                throw new Exception("la comision ya esta activa");

            comission.IsActive = false;
            comission.UpdatedAt = DateTime.Now;
            comission.DeactivatedAt = DateTime.Now;

            _context.Entry(comission).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }
       
        public async Task<bool> ActivateComissionAsync(int id)
        {
            var comission = await _context.Comissions.FindAsync(id);

            if (comission is null)
                throw new Exception("comision no encontrada");

            if (comission.IsActive)
                throw new Exception("la comision ya esta activa");

            comission.IsActive = true;
            comission.UpdatedAt = DateTime.Now;
            comission.DeactivatedAt = null;

            _context.Entry(comission).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

       
    }
}
