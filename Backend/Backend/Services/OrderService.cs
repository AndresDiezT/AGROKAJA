using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.DTOs.OrderDTOs;
using Backend.DTOs.ComissionDTOs;
using Backend.Interfaces;
using System.Net;


namespace Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly BackendDbContext _context;

        public OrderService(BackendDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var order = await _context.Orders.ToListAsync();

            return order;
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(e => e.IdOrder == id);
            if (order is null)
            {
                throw new Exception("order no encontrada");
            }
            return order;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
        {
            if (await _context.Orders.AnyAsync(e => e.TotalAmountOrder == dto.TotalAmountOrder))
                throw new Exception("orden ya registrada");

            var newOrder = new Order
            {
                DateOrder =DateTime.Now,
                IdPaymentMethod = dto.IdPaymentMethod,
                UserDocument = dto.UserDocument,
                IdAddress = dto.IdAddress,
                IdOrderStatus = dto.IdOrderStatus,
                IdComission = dto.IdComission,
                IdPaymentStatus = dto.IdPaymentStatus,
                TotalAmountOrder = dto.TotalAmountOrder,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            _context.Orders.Add(newOrder);

            await _context.SaveChangesAsync();

            return newOrder;
        }
        public async Task<Order> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            var existingOrder = await _context.Orders.FindAsync(id);

            if (existingOrder is null)
                throw new Exception("orden no encontrada");

            existingOrder.TotalAmountOrder = dto.TotalAmountOrder;
            existingOrder.UpdatedAt = DateTime.Now;

            _context.Entry(existingOrder).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<bool> DeactivateOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order is null)
                throw new Exception("orden no encontrada");

            if (!order.IsActive)
                throw new Exception("la orden ya esta activa");

            order.IsActive = false;
            order.UpdatedAt = DateTime.Now;
            order.DeactivatedAt = DateTime.Now;

            _context.Entry(order).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order is null)
                throw new Exception("orden no encontrada");

            if (order.IsActive)
                throw new Exception("la orden ya esta activa");

            order.IsActive = true;
            order.UpdatedAt = DateTime.Now;
            order.DeactivatedAt = null;

            _context.Entry(order).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
