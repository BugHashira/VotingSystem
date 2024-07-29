using VotingSystem.Dto;
using VotingSystem.Dto.Manifestoes;

namespace VotingSystem.Services.Interface
{
    public interface IManifestoService
    {
        Task<BaseResponseModel<bool>> AddManifestoAsync(CreateManifestoDto request);
        Task<BaseResponseModel<bool>> DeleteManifestoAsync(int id);
        Task<BaseResponseModel<IEnumerable<ManifestoDto>>> GetAllManifestosAsync();
        Task<BaseResponseModel<ManifestoDto>> GetManifestoByIdAsync(Guid id);
        Task<BaseResponseModel<bool>> UpdateManifestoAsync(Guid id, UpdateManifestoDto request);
    }
}
