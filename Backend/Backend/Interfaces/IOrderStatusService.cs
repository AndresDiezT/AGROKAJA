using Backend.DTOs.OrderDTOs;
using Backend.DTOs.OrderStatusDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusAsync();
        Task<OrderStatus> GetOrderStatusByIdAsync(int id);
        Task<OrderStatus> CreateOrderStatusAsync(SaveOrderStatusDto dto);
        Task<OrderStatus> UpdateOrderStatusAsync(int id, SaveOrderStatusDto dto);
        Task<bool> DeactivateOrderStatusAsync(int id);
        Task<bool> ActivateOrderStatusAsync(int id);
    }
}
