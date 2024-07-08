using VotingSystem.Data.Dto.Candidates;

namespace VotingSystem.Services.Interface
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetAllCandidatesAsync();
        Task<CandidateDto> GetCandidateByIdAsync(Guid id);
        Task AddCandidateAsync(CreateCandidateDto request);
        Task UpdateCandidateAsync(Guid id, UpdateCandidateDto request);
        Task DeleteCandidateAsync(int id);
    }
}
