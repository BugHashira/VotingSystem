using VotingSystem.Models;

namespace VotingSystem.Data.Entities
{
    public class PaymentInvoice : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public string PaymentEvidence { get; set; }
        public Candidate Candidate { get; set; }
    }
}
