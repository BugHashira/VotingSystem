using Microsoft.AspNetCore.Mvc.Rendering;
using VotingSystem.Dto;
using VotingSystem.Dto.Candidates;

namespace VotingSystem.Services.Interface
{
    public interface ICandidateService
    {
        Task<BaseResponseModel<bool>> AddCandidateAsync(CreateCandidateDto request);
        Task<BaseResponseModel<bool>> DeleteCandidateAsync(int id);
        Task<BaseResponseModel<IEnumerable<CandidateDto>>> GetAllCandidatesAsync(Guid electionId);
        Task<BaseResponseModel<CandidateDto>> GetCandidateByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdateCandidateAsync(Guid id, UpdateCandidateDto request);
        Task<IEnumerable<SelectListItem>> GetCandidateListItem();
    }
}
