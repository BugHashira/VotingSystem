using VotingSystem.Dto.Manifestoes;

namespace VotingSystem.Services.Interface
{
    public interface IManifestoService
    {
        Task<IEnumerable<ManifestoDto>> GetAllManifestosAsync();
        Task<ManifestoDto> GetManifestoByIdAsync(Guid id);
        Task AddManifestoAsync(CreateManifestoDto request);
        Task UpdateManifestoAsync(Guid id, UpdateManifestoDto request);
        Task DeleteManifestoAsync(int id);
    }
}
