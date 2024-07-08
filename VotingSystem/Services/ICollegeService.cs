using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services
{
    public interface ICollegeService
    {
        Task<IEnumerable<College>> GetAllCollegesAsync();
        Task<College> GetCollegeByIdAsync(int id);
        Task AddCollegeAsync(College college);
        Task UpdateCollegeAsync(College college);
        Task DeleteCollegeAsync(int id);
    }
}
