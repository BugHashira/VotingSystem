using VotingSystem.Dto;
using VotingSystem.Dto.Votes;

namespace VotingSystem.Services.Interface
{
    public interface IVoteService
    {
        Task<BaseResponseModel<bool>> AddVoteAsync(CreateVoteDto request);
        Task<BaseResponseModel<bool>> DeleteVoteAsync(int id);
        Task<BaseResponseModel<IEnumerable<VoteDto>>> GetAllVotesAsync();
        Task<BaseResponseModel<VoteDto>> GetVoteByIdAsync(string id);
        Task<BaseResponseModel<bool>> UpdateVoteAsync(Guid id, UpdateVoteDto request);
    }
}
