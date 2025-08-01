using Backend.DTOs.ComissionDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IInvoiceService
    {
        Task<Invoice> GetInvoiceByNumberAsync(string invoiceNumber);
        Task<IEnumerable<Invoice>> GetInvoiceByUserAsync(string userDocument);

    }
}
