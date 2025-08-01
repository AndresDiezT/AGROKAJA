using Backend.DTOs.OrderDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<Order> GetOrderDetailByIdAsync(int id);
        Task<Order> CreateOrderDetailAsync(CreateOrderDto dto);
        Task<Order> UpdateOrderDetailAsync(int id, UpdateOrderDto dto);
        Task<bool> DeactivateOrderDetailAsync(int id);
        Task<bool> ActivateOrderDetailAsync(int id);
    }
}
