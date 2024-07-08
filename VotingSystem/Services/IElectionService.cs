using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services
{
    public interface IElectionService
    {
        Task<IEnumerable<Election>> GetAllElectionsAsync();
        Task<Election> GetElectionByIdAsync(int id);
        Task AddElectionAsync(Election election);
        Task UpdateElectionAsync(Election election);
        Task DeleteElectionAsync(int id);
    }
}
