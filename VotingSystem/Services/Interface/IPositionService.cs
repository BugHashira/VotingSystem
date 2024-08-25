using VotingSystem.Dto;
using VotingSystem.Dto.Positions;

namespace VotingSystem.Services.Interface
{
    public interface IPositionService
    {
        Task<BaseResponseModel<bool>> AddPositionAsync(CreatePositionDto request);
        Task<BaseResponseModel<bool>> DeletePositionAsync(Guid id);
        Task<BaseResponseModel<IEnumerable<PositionDto>>> GetAllPositionsAsync();
        Task<BaseResponseModel<IEnumerable<SelectPositionDto>>> GetPositionSelectAsync();
        Task<BaseResponseModel<PositionDto>> GetPositionByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdatePositionAsync(Guid id, UpdatePositionDto request);
    }
}
