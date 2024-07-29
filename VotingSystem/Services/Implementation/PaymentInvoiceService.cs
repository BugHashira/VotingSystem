using VotingSystem.Data;
using VotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Services.Interface;
using VotingSystem.Dto.PaymentInvoices;
using VotingSystem.Dto;

namespace VotingSystem.Services
{
    public class PaymentInvoiceService : IPaymentInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public PaymentInvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<IEnumerable<PaymentInvoiceDto>>> GetAllPaymentInvoicesAsync()
        {
            try
            {
                var paymentInvoices = await _context.PaymentInvoices
                    .Include(x => x.Candidate)
                    .Select(x => new PaymentInvoiceDto
                    {
                        Id = x.Id,
                        CandidateId = x.CandidateId,
                        PaymentEvidence = x.PaymentEvidence
                    }).ToListAsync();

                if (paymentInvoices.Count > 0)
                {
                    return new BaseResponseModel<IEnumerable<PaymentInvoiceDto>>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = paymentInvoices
                    };
                }

                return new BaseResponseModel<IEnumerable<PaymentInvoiceDto>>()
                {
                    IsSuccessful = false,
                    Message = "No record found",
                    Data = paymentInvoices
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<IEnumerable<PaymentInvoiceDto>>()
                {
                    IsSuccessful = false,
                    Message = "PaymentInvoiceService : GetAllPaymentInvoicesAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<PaymentInvoiceDto>> GetPaymentInvoiceByIdAsync(Guid id)
        {
            try
            {
                var paymentInvoice = await _context.PaymentInvoices
                    .Include(x => x.Candidate)
                    .Where(x => x.Id.Equals(id))
                    .Select(x => new PaymentInvoiceDto
                    {
                        Id = x.Id,
                        CandidateId = x.CandidateId,
                        PaymentEvidence = x.PaymentEvidence
                    }).FirstOrDefaultAsync();

                if (paymentInvoice != null)
                {
                    return new BaseResponseModel<PaymentInvoiceDto>()
                    {
                        IsSuccessful = true,
                        Message = "Data retrieved successfully",
                        Data = paymentInvoice
                    };
                }

                return new BaseResponseModel<PaymentInvoiceDto>
                {
                    IsSuccessful = false,
                    Message = "No record found"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<PaymentInvoiceDto>()
                {
                    IsSuccessful = false,
                    Message = "PaymentInvoiceService : GetPaymentInvoiceByIdAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> AddPaymentInvoiceAsync(CreatePaymentInvoiceDto request)
        {
            try
            {
                var paymentInvoice = new PaymentInvoice()
                {
                    Id = Guid.NewGuid(),
                    PaymentEvidence = request.PaymentEvidence,
                    CandidateId = request.CandidateId,
                    CreatedDate = DateTime.UtcNow
                };

                await _context.PaymentInvoices.AddAsync(paymentInvoice);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data Created successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Create failed",
                    Data = false
                };
            }
            catch (Exception)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "PaymentInvoiceService : AddPaymentInvoiceAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> UpdatePaymentInvoiceAsync(Guid id, UpdatePaymentInvoiceDto request)
        {
            try
            {
                var paymentInvoiceExist = await _context.PaymentInvoices.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (paymentInvoiceExist == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                paymentInvoiceExist.PaymentEvidence = request.PaymentEvidence;
                paymentInvoiceExist.CandidateId = request.CandidateId;
                paymentInvoiceExist.ModifiedDate = DateTime.Now;

                _context.PaymentInvoices.Update(paymentInvoiceExist);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data Updated successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Update failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "PaymentInvoiceService : UpdatePaymentInvoiceAsync : Error Occurred:"
                };
            }
        }

        public async Task<BaseResponseModel<bool>> DeletePaymentInvoiceAsync(int id)
        {
            try
            {
                var paymentInvoice = await _context.PaymentInvoices.FindAsync(id);

                if (paymentInvoice == null)
                    return new BaseResponseModel<bool>() { IsSuccessful = false, Message = "No record found", Data = false };

                _context.PaymentInvoices.Remove(paymentInvoice);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>()
                    {
                        IsSuccessful = true,
                        Message = "Data Deleted successfully",
                        Data = true
                    };
                }

                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "Delete failed",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>()
                {
                    IsSuccessful = false,
                    Message = "PaymentInvoiceService : DeletePaymentInvoiceAsync : Error Occurred:"
                };
            }
        }
    }
}
