using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingSystem.Data.Dto.College;
using VotingSystem.Data.Dto.Colleges;
using VotingSystem.Data.Dto.Manifestoes;
using VotingSystem.Data.Dto.Manifesto;

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
