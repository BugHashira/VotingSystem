using VotingSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingSystem.Services.Interface
{
    public interface IManifestoService
    {
        Task<IEnumerable<Manifesto>> GetAllManifestosAsync();
        Task<Manifesto> GetManifestoByIdAsync(int id);
        Task AddManifestoAsync(Manifesto manifesto);
        Task UpdateManifestoAsync(Manifesto manifesto);
        Task DeleteManifestoAsync(int id);
    }
}
