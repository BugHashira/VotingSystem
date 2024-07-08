using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services.Interface
{
    public interface ICandidateService
    {
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Task<Candidate> GetCandidateByIdAsync(int id);
        Task AddCandidateAsync(Candidate candidate);
        Task UpdateCandidateAsync(Candidate candidate);
        Task DeleteCandidateAsync(int id);
    }
}
