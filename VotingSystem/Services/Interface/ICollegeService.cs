using VotingSystem.Dto.Colleges;

namespace VotingSystem.Services.Interface
{
    public interface ICollegeService
    {
        Task<IEnumerable<CollegeDto>> GetAllCollegesAsync();
        Task<CollegeDto> GetCollegeByIdAsync(Guid id);
        Task AddCollegeAsync(CreateCollegeDto request);
        Task UpdateCollegeAsync(Guid id, UpdateCollegeDto request);
        Task DeleteCollegeAsync(int id);
    }
}
