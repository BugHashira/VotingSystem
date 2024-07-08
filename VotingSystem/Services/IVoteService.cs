using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services
{
    public interface IVoteService
    {
        Task<IEnumerable<Vote>> GetAllVotesAsync();
        Task<Vote> GetVoteByIdAsync(int id);
        Task AddVoteAsync(Vote vote);
        Task UpdateVoteAsync(Vote vote);
        Task DeleteVoteAsync(int id);
    }
}
