using Backend.Data;
using Backend.DTOs;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly BackendDbContext _context;

        public InvoiceService(BackendDbContext dbContext)
        {
            _context = dbContext;
        }


        public async Task<Invoice> GetInvoiceByNumberAsync(string invoiceNumber)

        {
            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(c => c.InvoiceNumber == invoiceNumber);

            if (invoice == null)
            {
                throw new Exception("Factura no encontrada");
            }

            return invoice;
        }
        public async Task<IEnumerable<Invoice>> GetInvoiceByUserAsync(string document)
        {
            var invoices= await _context.Invoices 
                .Where(inv => inv.Order.User.Document == document)
                .ToListAsync();

            if (invoices == null)
            {
                throw new Exception("Factura no encontrada");
            }

            return invoices;
        }


    }
}
