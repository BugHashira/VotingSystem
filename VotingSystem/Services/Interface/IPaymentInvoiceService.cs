using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Data.Dto.College;
using VotingSystem.Data.Dto.Colleges;
using VotingSystem.Data.Dto.PaymentInvoices;
using VotingSystem.Data.Dto.PaymentInvoice;

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
