using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Data.Dto.College;
using VotingSystem.Data.Dto.Colleges;
using VotingSystem.Data.Dto.Elections;
using VotingSystem.Data.Dto.Election;

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
