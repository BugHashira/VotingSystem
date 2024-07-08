using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services
{
    public class PaymentInvoiceService : IPaymentInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public PaymentInvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentInvoice>> GetAllInvoicesAsync()
        {
            return await _context.PaymentInvoices.ToListAsync();
        }

        public async Task<PaymentInvoice> GetInvoiceByIdAsync(int id)
        {
            return await _context.PaymentInvoices.FindAsync(id);
        }

        public async Task AddInvoiceAsync(PaymentInvoice invoice)
        {
            _context.PaymentInvoices.Add(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(PaymentInvoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.PaymentInvoices.FindAsync(id);
            if (invoice != null)
            {
                _context.PaymentInvoices.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
