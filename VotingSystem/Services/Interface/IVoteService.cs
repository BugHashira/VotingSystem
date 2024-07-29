using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Data.Dto.Position;
using VotingSystem.Data.Dto.Positions;
using VotingSystem.Data.Dto.Votes;
using VotingSystem.Data.Dto.Vote;

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
