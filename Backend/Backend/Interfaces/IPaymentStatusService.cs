using Backend.DTOs.ComissionDTOs;
using Backend.DTOs.PaymentStatusDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IPaymentStatusService
    {
        Task<IEnumerable<PaymentStatus>> GetAllPaymentStatusAsync();
        Task<PaymentStatus> GetPaymentStatusByIdAsync(int id);
        Task<PaymentStatus> CreatePaymentStatusAsync(SavePaymentStatusDto dto);
        Task<PaymentStatus> UpdatePaymentStatusAsync(int id, SavePaymentStatusDto dto);
        Task<bool> DeactivatePaymentStatusAsync(int id);
        Task<bool> ActivatePaymentStatusAsync(int id);
    }
}

