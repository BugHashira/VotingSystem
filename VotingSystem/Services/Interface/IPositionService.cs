using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Data.Dto.College;
using VotingSystem.Data.Dto.Colleges;
using VotingSystem.Data.Dto.Positions;
using VotingSystem.Data.Dto.Position;

namespace VotingSystem.Services.Interface
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionDto>> GetAllPositionsAsync();
        Task<PositionDto> GetPositionByIdAsync(Guid id);
        Task AddPositionAsync(CreatePositionDto request);
        Task UpdatePositionAsync(Guid id, UpdatePositionDto request);
        Task DeletePositionAsync(int id);
    }
}
