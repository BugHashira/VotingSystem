using VotingSystem.Dto;
using VotingSystem.Dto.Positions;

namespace VotingSystem.Services.Interface
{
    public interface IPositionService
    {
        Task<BaseResponseModel<bool>> AddPositionAsync(CreatePositionDto request);
        Task<BaseResponseModel<bool>> DeletePositionAsync(int id);
        Task<BaseResponseModel<IEnumerable<PositionDto>>> GetAllPositionsAsync();
        Task<BaseResponseModel<PositionDto>> GetPositionByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdatePositionAsync(Guid id, UpdatePositionDto request);
    }
}
