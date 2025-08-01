using Backend.Data;
using Backend.DTOs;
using Backend.DTOs.ComissionDTOs;
using Backend.DTOs.PaymentStatusDTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class PaymentStatusService : IPaymentStatusService
    {
        private readonly BackendDbContext _context;

        public PaymentStatusService(BackendDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<PaymentStatus>> GetAllPaymentStatusAsync()
        {
            var paymentStatus = await _context.PaymentStatuses.ToListAsync();
            return paymentStatus;
        }
        public async Task<PaymentStatus> GetPaymentStatusByIdAsync(int id)
        {
            var paymentStatus = await _context.PaymentStatuses.FindAsync(id);

            if (paymentStatus == null)
            {
                throw new Exception($"No se encontró un estado de pago con ID {id}");
            }

            return paymentStatus;
        }

        public async Task<PaymentStatus> CreatePaymentStatusAsync(SavePaymentStatusDto dto)
        {
            var paymentStatus = new PaymentStatus
            {
                NamePaymentStatus = dto.NamePaymentStatus,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.PaymentStatuses.Add(paymentStatus);
            await _context.SaveChangesAsync();

            return paymentStatus;
        }


        public async Task<PaymentStatus> UpdatePaymentStatusAsync(int id, SavePaymentStatusDto dto)
        {
            var paymentStatus = await _context.PaymentStatuses.FindAsync(id);

            if (paymentStatus == null)
            {
                throw new Exception($"No se encontró un estado de pago con ID {id}");
            }

            paymentStatus.NamePaymentStatus = dto.NamePaymentStatus;
            paymentStatus.UpdatedAt = DateTime.UtcNow;

            _context.PaymentStatuses.Update(paymentStatus);
            await _context.SaveChangesAsync();

            return paymentStatus;
        }

        public async Task<bool> DeactivatePaymentStatusAsync(int id)
        {
            var paymentStatus = await _context.PaymentStatuses.FindAsync(id);

            if (paymentStatus == null)
            {
                throw new Exception($"No se encontró un estado de pago con ID {id}");
            }

            if (!paymentStatus.IsActive)
            {
                // Ya está desactivado
                return false;
            }

            paymentStatus.IsActive = false;
            paymentStatus.DeactivatedAt = DateTime.UtcNow;

            _context.PaymentStatuses.Update(paymentStatus);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivatePaymentStatusAsync(int id)
        {
            var paymentStatus = await _context.PaymentStatuses.FindAsync(id);

            if (paymentStatus == null)
            {
                throw new Exception($"No se encontró un estado de pago con ID {id}");
            }

            if (paymentStatus.IsActive)
            {
                // Ya está activo, no es necesario hacer nada
                return false;
            }

            paymentStatus.IsActive = true;
            paymentStatus.DeactivatedAt = null; // Limpia la fecha de desactivación
            paymentStatus.UpdatedAt = DateTime.Now;

            _context.PaymentStatuses.Update(paymentStatus);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
