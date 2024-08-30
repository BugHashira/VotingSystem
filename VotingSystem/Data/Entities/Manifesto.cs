using System.ComponentModel.DataAnnotations;
using VotingSystem.Models;

namespace VotingSystem.Data.Entities
{
    public class Manifesto : BaseEntity
    {
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public byte[] ManifestoDocument { get; set; }
        public string? FileName { get; set; }
        public string? FileExtension { get; set; }
    }
}
