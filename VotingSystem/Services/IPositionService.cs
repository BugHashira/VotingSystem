using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task<Position> GetPositionByIdAsync(int id);
        Task AddPositionAsync(Position position);
        Task UpdatePositionAsync(Position position);
        Task DeletePositionAsync(int id);
    }
}
