using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Dto.Elections;

namespace VotingSystem.Services.Interface
{
    public interface IElectionService
    {
        Task<IEnumerable<ElectionDto>> GetAllElectionsAsync();
        Task<ElectionDto> GetElectionByIdAsync(Guid id);
        Task AddElectionAsync(CreateElectionDto request);
        Task UpdateElectionAsync(Guid id, UpdateElectionDto request);
        Task DeleteElectionAsync(int id);
    }
}
