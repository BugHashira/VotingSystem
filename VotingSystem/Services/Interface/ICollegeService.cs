using VotingSystem.Dto;
using VotingSystem.Dto.Colleges;

namespace VotingSystem.Services.Interface
{
    public interface ICollegeService
    {
        Task<BaseResponseModel<bool>> AddCollegeAsync(CreateCollegeDto request);
        Task<BaseResponseModel<bool>> DeleteCollegeAsync(int id);
        Task<BaseResponseModel<IEnumerable<CollegeDto>>> GetAllCollegesAsync();
        Task<BaseResponseModel<CollegeDto>> GetCollegeByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdateCollegeAsync(Guid id, UpdateCollegeDto request);
    }
}
