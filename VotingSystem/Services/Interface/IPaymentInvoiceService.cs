using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Dto.PaymentInvoices;
using VotingSystem.Dto;


namespace VotingSystem.Services.Interface
{
    public interface IPaymentInvoiceService
    {
        Task<BaseResponseModel<bool>> AddPaymentInvoiceAsync(CreatePaymentInvoiceDto request);
        Task<BaseResponseModel<bool>> DeletePaymentInvoiceAsync(int id);
        Task<BaseResponseModel<IEnumerable<PaymentInvoiceDto>>> GetAllPaymentInvoicesAsync();
        Task<BaseResponseModel<PaymentInvoiceDto>> GetPaymentInvoiceByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdatePaymentInvoiceAsync(Guid id, UpdatePaymentInvoiceDto request);
    }
}
