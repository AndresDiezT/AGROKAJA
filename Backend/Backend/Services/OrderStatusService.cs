using Backend.Data;
using Backend.DTOs.ComissionDTOs;
using Backend.DTOs.OrderStatusDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly BackendDbContext _context;

        public OrderStatusService(BackendDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync()
        {
            var orderStatus = await _context.OrderStatus.ToListAsync();

            return orderStatus;
        }
        public async Task<OrderStatus> GetOrderStatusByIdAsync(int id)
        {
            var orderStatus = await _context.OrderStatus
                .FirstOrDefaultAsync(r => r.IdOrderStatus == id);
            if (orderStatus is null)
            {
                throw new Exception("estado de la orden no encontrada");
            }
            return orderStatus;
        }

        public async Task<OrderStatus> CreateOrderStatusAsync(SaveOrderStatusDto dto)
        {
            if (await _context.OrderStatus.AnyAsync(r => r.NameOrderStatus == dto.NameOrderStatus))
                throw new Exception("estado de la orden ya registrada");

            var newOrderStatus = new OrderStatus
            {
                NameOrderStatus = dto.NameOrderStatus,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.OrderStatus.Add(newOrderStatus);

            await _context.SaveChangesAsync();

            return newOrderStatus;
        }
        public async Task<OrderStatus> UpdateOrderStatusAsync(int id, SaveOrderStatusDto dto)
        {
            var existingOrderStatus = await _context.OrderStatus.FindAsync(id);

            if (existingOrderStatus is null)
                throw new Exception("estado de la orden no encontrado");

            existingOrderStatus.NameOrderStatus = dto.NameOrderStatus;
            existingOrderStatus.UpdatedAt = DateTime.Now;

            _context.Entry(existingOrderStatus).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return existingOrderStatus;
        }

        public async Task<bool> DeactivateOrderStatusAsync(int id)
        {
            var orderStatus = await _context.OrderStatus.FindAsync(id);

            if (orderStatus is null)
                throw new Exception("estado de la orden no encontrado");

            if (!orderStatus.IsActive)
                throw new Exception("el estado de la orden ya esta activo");

            orderStatus.IsActive = false;
            orderStatus.UpdatedAt = DateTime.Now;
            orderStatus.DeactivatedAt = DateTime.Now;

            _context.Entry(orderStatus).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateOrderStatusAsync(int id)
        {
            var orderStatus = await _context.OrderStatus.FindAsync(id);

            if (orderStatus is null)
                throw new Exception("estado de la orden no encontrado");

            if (orderStatus.IsActive)
                throw new Exception("el estado de la orden ya esta activo");

            orderStatus.IsActive = true;
            orderStatus.UpdatedAt = DateTime.Now;
            orderStatus.DeactivatedAt = null;

            _context.Entry(orderStatus).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
