using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Dto.Elections;
using VotingSystem.Dto;

namespace VotingSystem.Services.Interface
{
    public interface IElectionService
    {
        Task<BaseResponseModel<bool>> AddElectionAsync(CreateElectionDto request);
        Task<BaseResponseModel<bool>> DeleteElectionAsync(Guid id);
        Task<BaseResponseModel<IEnumerable<ElectionDto>>> GetAllElectionsAsync();
        Task<BaseResponseModel<ElectionDto>> GetElectionByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdateElectionAsync(Guid id, UpdateElectionDto request);
    }
}
