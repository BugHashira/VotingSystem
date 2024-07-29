using VotingSystem.Data.Entities;

namespace VotingSystem.Data.Dto.PaymentInvoices
{
    public class PaymentInvoiceDto
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string PaymentEvidence { get; set; }
        public string CandidateName { get; set; }
    }
}
