using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Dto.PaymentInvoices;


namespace VotingSystem.Services.Interface
{
    public interface IPaymentInvoiceService
    {
        Task<IEnumerable<PaymentInvoiceDto>> GetAllPaymentInvoicesAsync();
        Task<PaymentInvoiceDto> GetPaymentInvoiceByIdAsync(Guid id);
        Task AddPaymentInvoiceAsync(CreatePaymentInvoiceDto request);
        Task UpdatePaymentInvoiceAsync(Guid id, UpdatePaymentInvoiceDto request);
        Task DeletePaymentInvoiceAsync(int id);
    }
}
