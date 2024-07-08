using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services
{
    public interface IPaymentInvoiceService
    {
        Task<IEnumerable<PaymentInvoice>> GetAllInvoicesAsync();
        Task<PaymentInvoice> GetInvoiceByIdAsync(int id);
        Task AddInvoiceAsync(PaymentInvoice invoice);
        Task UpdateInvoiceAsync(PaymentInvoice invoice);
        Task DeleteInvoiceAsync(int id);
    }
}
