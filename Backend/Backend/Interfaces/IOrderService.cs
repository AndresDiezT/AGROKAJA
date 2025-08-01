using Backend.DTOs.OrderDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(CreateOrderDto dto);
        Task<Order> UpdateOrderAsync(int id, UpdateOrderDto dto);
        Task<bool> DeactivateOrderAsync(int id);
        Task<bool> ActivateOrderAsync(int id);
    }
}
