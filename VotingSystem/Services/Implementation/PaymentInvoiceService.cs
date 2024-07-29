using VotingSystem.Data;
using VotingSystem.Data.Entities;
using VotingSystem.Services.Interface;
using VotingSystem.Data.Dto.PaymentInvoice;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Data.Dto.PaymentInvoices;

namespace VotingSystem.Services
{
    public class PaymentInvoiceService : IPaymentInvoiceService
    {
        private readonly ApplicationDbContext _context;

        public PaymentInvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentInvoiceDto>> GetAllPaymentInvoicesAsync()
        {
            return await _context.PaymentInvoices
                .Include(x => x.Candidate)
                .Select(x => new PaymentInvoiceDto
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    PaymentEvidence = x.PaymentEvidence
                }).ToListAsync();
        }

        public async Task<PaymentInvoiceDto> GetPaymentInvoiceByIdAsync(Guid id)
        {
            return await _context.PaymentInvoices
                .Include(x => x.Candidate)
                .Where(x => x.Id.Equals(id))
                .Select(x => new PaymentInvoiceDto
                {
                    Id = x.Id,
                    CandidateId = x.CandidateId,
                    PaymentEvidence = x.PaymentEvidence
                }).FirstOrDefaultAsync();
        }

        public async Task AddPaymentInvoiceAsync(CreatePaymentInvoiceDto request)
        {
            var paymentInvoice = new PaymentInvoice
            {
                Id = Guid.NewGuid(),
                PaymentEvidence = request.PaymentEvidence,
                CandidateId = request.CandidateId,
                CreatedDate = DateTime.UtcNow
            };

            _context.PaymentInvoices.Add(paymentInvoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentInvoiceAsync(Guid id, UpdatePaymentInvoiceDto request)
        {
            var paymentInvoiceExist = await _context.PaymentInvoices.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (paymentInvoiceExist != null)
            {
                paymentInvoiceExist.PaymentEvidence = request.PaymentEvidence;
                paymentInvoiceExist.CandidateId = request.CandidateId;
                paymentInvoiceExist.ModifiedDate = DateTime.Now;

                _context.PaymentInvoices.Update(paymentInvoiceExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePaymentInvoiceAsync(int id)
        {
            var paymentInvoice = await _context.PaymentInvoices.FindAsync(id);
            if (paymentInvoice != null)
            {
                _context.PaymentInvoices.Remove(paymentInvoice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
