using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.PaymentInvoices
{
    public class UpdatePaymentInvoiceDto
    {
        [Required(ErrorMessage = "Candidate Id is required")]
        public Guid CandidateId { get; set; }

        [Required(ErrorMessage = "Payment Evidence is required")]
        public string PaymentEvidence { get; set; }
    }
}
