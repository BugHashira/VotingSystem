using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.PaymentInvoice
{
    public class CreatePaymentInvoiceDto
    {
        [Required(ErrorMessage = "Candidate Id is required")]
        public Guid CandidateId { get; set; }

        [Required(ErrorMessage = "Payment Evidence is required")]
        public string PaymentEvidence { get; set; }
    }
}