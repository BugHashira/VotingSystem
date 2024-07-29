using VotingSystem.Dto.Positions;

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
