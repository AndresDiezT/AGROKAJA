using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.DepartmentDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly BackendDbContext _context;

        public DepartmentService(BackendDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Department>>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .Include(d => d.Country)
                .ToListAsync();

            return Result<IEnumerable<Department>>.Ok(departments);
        }

        public async Task<Result<Department>> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Country)
                .FirstOrDefaultAsync(d => d.IdDepartment == id);

            if (department is null)
                return Result<Department>.Fail("Departamento no encontrado");

            return Result<Department>.Ok(department);
        }

        public async Task<Result<Department>> CreateDepartmentAsync(CreateDepartmentDto createDepartmentDto)
        {
            if (await _context.Departments.AnyAsync(u => u.NameDepartment == createDepartmentDto.NameDepartment))
                return Result<Department>.Fail("El departamento ya esta registrado");

            var department = new Department
            {
                NameDepartment = createDepartmentDto.NameDepartment,
                IdCountry = createDepartmentDto.IdCountry,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Departments.Add(department);

            await _context.SaveChangesAsync();

            return Result<Department>.Ok(department);
        }

        public async Task<Result<Department>> UpdateDepartmentAsync(UpdateDepartmentDto updateDepartmentDto)
        {
            var existingDepartment = await _context.Departments.FindAsync(updateDepartmentDto.IdDepartment);

            if (existingDepartment is null)
                return Result<Department>.Fail("Departamento no encontrado");

            existingDepartment.NameDepartment = updateDepartmentDto.NameDepartment;
            existingDepartment.IdCountry = updateDepartmentDto.IdCountry;
            existingDepartment.UpdatedAt = DateTime.Now;

            _context.Entry(existingDepartment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<Department>.Ok(existingDepartment);
        }

        public async Task<Result<bool>> DeactivateDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department is null)
                return Result<bool>.Fail("Departamento no encontrado");

            if (!department.IsActive)
                return Result<bool>.Fail("El departamento ya esta inactivo");

            department.IsActive = false;
            department.UpdatedAt = DateTime.Now;
            department.DeactivatedAt = DateTime.Now;

            _context.Entry(department).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ActivateDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department is null)
                return Result<bool>.Fail("Departamento no encontrado");

            if (department.IsActive)
                return Result<bool>.Fail("El departamento ya esta activo");

            department.IsActive = true;
            department.UpdatedAt = DateTime.Now;
            department.DeactivatedAt = null;

            _context.Entry(department).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
