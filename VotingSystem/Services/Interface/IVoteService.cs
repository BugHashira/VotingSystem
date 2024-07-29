using VotingSystem.Dto.Votes;

namespace VotingSystem.Services.Interface
{
    public interface IVoteService
    {
        Task<IEnumerable<VoteDto>> GetAllVotesAsync();
        Task<VoteDto> GetVoteByIdAsync(Guid id);
        Task AddVoteAsync(CreateVoteDto request);
        Task UpdateVoteAsync(Guid id, UpdateVoteDto request);
        Task DeleteVoteAsync(int id);
    }
}
